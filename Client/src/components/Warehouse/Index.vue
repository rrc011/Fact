<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <el-row>
          <el-col :span="4" style="padding-top:10px">
            <span>Listado de Almacenes</span>
          </el-col>
          <el-col :span="16">
            <el-input placeholder="Buscar..." v-model="search" class="input-with-select">
              <el-button
                slot="append"
                icon="el-icon-search"
                @click="getAll(1, search)"
              ></el-button>
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-button
              size="small"
              @click="$router.push(`/warehouse/add`)"
              icon="el-icon-plus"
              style="float: right; margin-top: -0.5%;"
              type="primary"
            >Nuevo</el-button>
          </el-col>
        </el-row>
      </div>
      <el-table v-loading="loading" :data="data.items" style="width: 100%">
        <el-table-column type="expand">
          <template slot-scope="props">
            <p>Descripcion: {{ props.row.description }}</p>
          </template>
        </el-table-column>
        <el-table-column prop="name" label="Nombre de Almacen"></el-table-column>
        <el-table-column prop="location" label="Ubicacion de Almacen"></el-table-column>
        <el-table-column>
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                icon="el-icon-edit-outline"
                @click="$router.push(`/warehouse/${scope.row.warehouseId}/edit`)"
              >Editar</el-button>
              <el-button
                icon="el-icon-delete"
                :loading="deleted"
                type="danger"
                @click="remove(scope.row.warehouseId, infoPaginacion.actual)"
              >Borrar</el-button>
            </el-button-group>
          </template>
        </el-table-column>
      </el-table>
      <div class="block mt-2">
        <nav>
          <ul class="pagination justify-content-end">
            <li class="page-item" v-if="infoPaginacion.actual > 1">
              <a
                class="page-link"
                href="#"
                @click.prevent="handleCurrentChange((infoPaginacion.actual - 1), search)"
              >Atras</a>
            </li>

            <li class="page-item" v-for="p in pagesNumber" :key="p" v-bind:class="isActive(p)">
              <a class="page-link" href="#" @click.prevent="handleCurrentChange(p, search)">{{p}}</a>
            </li>

            <li class="page-item" v-if="infoPaginacion.actual < infoPaginacion.total">
              <a
                class="page-link"
                href="#"
                @click.prevent="handleCurrentChange((infoPaginacion.actual + 1), search)"
              >Siguiente</a>
            </li>
          </ul>
        </nav>
      </div>
    </el-card>
  </div>
</template>

<script>
export default {
  name: "warehouse",
  data() {
    return {
      data: [],
      loading: false,
      deleted: false,
      infoPaginacion: {
        total: 0,
        actual: 0,
        anterior: null,
        siguiente: null
      },
      search: ""
    };
  },
  created() {
    this.getAll(
      parseInt(this.$route.params.page),
      ""
    );
  },
  computed: {
    pagesNumber: function() {
      if (!(this.infoPaginacion.total - (this.infoPaginacion.total - 1)))
        return [];

      var from = this.infoPaginacion.actual - 2;
      if (from < 1) from = 1;

      var to = from + 2 * 2;
      if (to >= this.infoPaginacion.total) to = this.infoPaginacion.total;

      var pagesArray = [];
      while (from <= to) {
        pagesArray.push(from);
        from++;
      }

      return pagesArray;
    }
  },
  methods: {
    getAll(page, search) {
      let self = this;
      self.loading = true;
      this.$store.state.services.WarehouseService
        .getAll(page, search)
        .then(r => {
          self.data = r.data;
          console.log(r.data);
          self.infoPaginacion.total = r.data.paginationInfo.totalPages;
          self.infoPaginacion.actual = r.data.paginationInfo.pageIndex;
          self.infoPaginacion.anterior = r.data.paginationInfo.hasPreviousPage;
          self.infoPaginacion.siguiente = r.data.paginationInfo.hasNextPage;
          self.loading = false;
        })
        .catch(r => {
          self.$message({
            message: "Ocurrio un error inesperado",
            type: "error"
          });
        });
    },
    remove(id, page) {
      let self = this;
      this.$confirm(
        "Esto borrará permanentemente el almacen. ¿Continuar?",
        "Cuidado!",
        {
          confirmButtonText: "Si",
          cancelButtonText: "Cancelar",
          type: "warning"
        }
      ).then(() => {
          _remove(page);
      }).catch(r => {
          self.$message({
            message: "Se cancelo la operacion",
            type: "warning"
          });
      }).finally(r => {
          this.deleted = false;
      });

      function _remove(_page) {
        self.delete = true;
        self.$store.state.services.WarehouseService.delete(id).then(r => {
          self.getAll(_page, "");
          if (r.data) {
            self.$message({
              message: "La Categoria se elimino con exito",
              type: "success"
            });
          } else {
            self.$message({
              message: "Ocurrio un error inesperado",
              type: "error"
            });
          }
        });
      }
    },
    next(page, search) {
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page, search);
      self.$router.push(`/warehouse/page/${parseInt(p) + 1}`);
    },
    prev(page, search) {
      let self = this;
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page, search);
      self.$router.push(`/warehouse/page/${parseInt(p) - 1}`);
    },
    handleCurrentChange(page, search) {
      let self = this;
      self.infoPaginacion.actual = page;
      self.$router.push(
        `/warehouse/page/${parseInt(self.infoPaginacion.actual)}`
      );
      this.getAll(page, search);
    },
    isActive: function(page) {
      return this.infoPaginacion.actual == page ? "active" : "";
    }
  }
};
</script>