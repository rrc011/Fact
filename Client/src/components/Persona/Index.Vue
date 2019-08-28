<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <el-row>
          <el-col :span="4" style="padding-top:10px">
            <span>Persona</span>
          </el-col>
          <el-col :span="16">
            <el-input placeholder="Buscar..." v-model="search" class="input-with-select">
              <el-button slot="append" icon="el-icon-search" @click="getAll(1, $route.params.tipo, search)"></el-button>
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-button
              size="small"
              @click="$router.push(`/persona/add/${$route.params.tipo}`)"
              icon="el-icon-plus"
              style="float: right; margin-top: -0.5%;"
              type="primary"
            >Nuevo</el-button>
          </el-col>
        </el-row>
      </div>
      <el-table v-loading="loading" :data="data.items" style="width: 100%">
        <el-table-column prop="cedula" label="Documento de Identidad"></el-table-column>
        <el-table-column prop="nombres" label="Nombres" sortable></el-table-column>
        <el-table-column prop="apellidos" label="Apellidos" sortable></el-table-column>
        <el-table-column>
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                icon="el-icon-edit-outline"
                :loading="deleted"
                @click="$router.push(`/persona/${scope.row.personaId}/edit`)"
              >Editar</el-button>
              <el-button
                icon="el-icon-delete"
                type="danger"
                @click="remove(scope.row.personaId, infoPaginacion.actual, $route.params.tipo)"
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
  name: "PersonaIndex",
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
    this.getAll(parseInt(this.$route.params.page), parseInt(this.$route.params.tipo), "");
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
    getAll(page, tipo, search) {
      let self = this;
      self.loading = true;
      this.$store.state.services.personaService
        .getAll(page, tipo, search)
        .then(r => {
          self.data = r.data;
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
    remove(id, page, tipo) {
      let self = this;
      self.delete = true;
      this.$confirm(
        "Esto borrará permanentemente la persona. ¿Continuar?",
        "Cuidado!",
        {
          confirmButtonText: "Si",
          cancelButtonText: "Cancelar",
          type: "warning"
        }
      ).then(() => {
          _remove(page, tipo);
          self.deleted = false;
        })
        .catch(r => {
          self.$message({
            message: "Se cancelo la operacion",
            type: "warning"
          });
        });

      function _remove(_page, tipo) {
        self.$store.state.services.personaService.delete(id).then(r => {
          self.getAll(_page, tipo, "");
          if(r.data){
            self.$message({
              message: "La Persona se elimino con exito",
              type: "success"
            });
          }else{
            self.$message({
              message: "Ocurrio un error inesperado",
              type: "error"
            });          
          }
        });
      }
    },
    next(page, tipo, search) {
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page,tipo, search);
      self.$router.push(`/persona/page/${parseInt(p) + 1}/${parseInt(tipo)}`);
    },
    prev(page, tipo, search) {
      let self = this;
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page, tipo, search);
      self.$router.push(`/persona/page/${parseInt(p) - 1}/${parseInt(tipo)}`);
    },
    handleCurrentChange(page, tipo, search) {
      let self = this;
      self.infoPaginacion.actual = page;
      self.$router.push(
        `/persona/page/${parseInt(self.infoPaginacion.actual)}/${parseInt(tipo)}`
      );
      this.getAll(page, tipo, search);
    },
    isActive: function(page) {
      return this.infoPaginacion.actual == page ? "active" : "";
    }
  }
};
</script>