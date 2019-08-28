<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <el-row>
          <el-col :span="4" style="padding-top:10px">
            <span>Estudiantes</span>
          </el-col>
          <el-col :span="16">
            <el-input placeholder="Buscar..." v-model="search" class="input-with-select">
              <el-button slot="append" icon="el-icon-search" @click="getAll(1, search)"></el-button>
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-button
              size="small"
              @click="$router.push('/students/add')"
              icon="el-icon-plus"
              style="float: right; margin-top: -0.5%;"
              type="primary"
            >Nuevo</el-button>
          </el-col>
        </el-row>
      </div>
      <el-table v-loading="loading" :data="data.items" style="width: 100%">
        <el-table-column prop="name" label="Nombres" sortable></el-table-column>
        <el-table-column prop="lastName" label="Apellidos" sortable></el-table-column>
        <el-table-column prop="bio" label="Biografia"></el-table-column>
        <el-table-column>
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                icon="el-icon-edit-outline"
                @click="$router.push(`/students/${scope.row.studentId}/edit`)"
              >Editar</el-button>
              <el-button
                icon="el-icon-delete"
                type="danger"
                @click="remove(scope.row.studentId, infoPaginacion.actual)"
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
  name: "StudentsIndex",
  data() {
    return {
      data: [],
      loading: false,
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
    let self = this;
    self.getAll(self.$route.params.page, "");
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
      self.$store.state.services.studentService
        .getAll(page, search)
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
    remove(id, page) {
      let self = this;

      this.$confirm(
        "Esto borrará permanentemente el estudiante. ¿Continuar?",
        "Cuidado!",
        {
          confirmButtonText: "Si",
          cancelButtonText: "Cancelar",
          type: "warning"
        }
      )
        .then(() => {
          _remove(page);
        })
        .catch(r => {
          self.$message({
            message: "Se cancelo la operacion",
            type: "warning"
          });
        });

      function _remove(_page) {
        self.$store.state.services.studentService.delete(id).then(r => {
          self.getAll(_page, "");
          self.$message({
            message: "Se elimino el estudiante con exito",
            type: "success"
          });
        });
      }
    },
    next(page, search) {
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page, search);
      self.$router.push(`/students/page/${parseInt(p) + 1}`);
    },
    prev(page, search) {
      let self = this;
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page, search);
      self.$router.push(`/students/page/${parseInt(p) - 1}`);
    },
    handleCurrentChange(page, search) {
      let self = this;
      self.infoPaginacion.actual = page;
      self.$router.push(
        `/students/page/${parseInt(self.infoPaginacion.actual)}`
      );
      this.getAll(page, search);
    },
    isActive: function(page) {
      return this.infoPaginacion.actual == page ? "active" : "";
    }
  }
};
</script>