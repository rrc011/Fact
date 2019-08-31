<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <el-row>
          <el-col :span="20" style="padding-top:10px">
            <span>Listado de Ventas</span>
          </el-col>
          <el-col :span="4">
            <el-button
              size="small"
              @click="$router.push(`/invoicing/add`)"
              icon="el-icon-plus"
              style="float: right; margin-top: -0.5%;"
              type="primary"
            >Nuevo</el-button>
          </el-col>
        </el-row>
      </div>
      <el-table v-loading="loading" :data="data.items" style="width: 100%">
        <el-table-column width="120" prop="orderId" label="Factura Nº"></el-table-column>
        <el-table-column width="220" prop="personName" label="Cliente"></el-table-column>
        <el-table-column width="120" prop="date" label="Fecha"></el-table-column>
        <el-table-column width="120" prop="total" label="Total"></el-table-column>
        <el-table-column>
          <template slot-scope="scope">
              <el-button
                icon="el-icon-edit-outline"
                @click="$router.push(`/invoicing/${scope.row.orderId}/detail`)"
              ></el-button>
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
                @click.prevent="handleCurrentChange((infoPaginacion.actual - 1))"
              >Atras</a>
            </li>

            <li class="page-item" v-for="p in pagesNumber" :key="p" v-bind:class="isActive(p)">
              <a class="page-link" href="#" @click.prevent="handleCurrentChange(p)">{{p}}</a>
            </li>

            <li class="page-item" v-if="infoPaginacion.actual < infoPaginacion.total">
              <a
                class="page-link"
                href="#"
                @click.prevent="handleCurrentChange((infoPaginacion.actual + 1))"
              >Siguiente</a>
            </li>
          </ul>
        </nav>
      </div>
    </el-card>

    <div class="sticky-top float-right mt-2 mr-1">
      <el-button-group>
        <el-button :loading="btnPDFStatus" type="danger" icon="el-icon-document" @click="exportPDF()">PDF</el-button>
        <el-button :loading="btnExcelStatus" type="success" icon="el-icon-paperclip" @click="exportExcel()">Excel</el-button>
      </el-button-group>
    </div>
  </div>
</template>

<script>
export default {
  name: "invoicing",
  data() {
    return {
      data: [],
      loading: false,
      btnPDFStatus: false,
      btnExcelStatus: false,
      deleted: false,
      infoPaginacion: {
        total: 0,
        actual: 0,
        anterior: null,
        siguiente: null
      }
    };
  },
  created() {
    this.getAll(
      parseInt(this.$route.params.page)
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
    getAll(page) {
      let self = this;
      self.loading = true;
      this.$store.state.services.InvoicingService
        .getAll(page)
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
            message: `${r}`,
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
        self.$store.state.services.InvoicingService.delete(id).then(r => {
          self.getAll(_page);
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
    next(page) {
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page);
      self.$router.push(`/client/page/${parseInt(p) + 1}`);
    },
    prev(page) {
      let self = this;
      var p = self.$route.params.page;
      this.infoPaginacion.actual = page;
      this.getAll(page);
      self.$router.push(`/client/page/${parseInt(p) - 1}`);
    },
    handleCurrentChange(page) {
      let self = this;
      self.infoPaginacion.actual = page;
      self.$router.push(
        `/client/page/${parseInt(self.infoPaginacion.actual)}`
      );
      this.getAll(page);
    },
    isActive: function(page) {
      return this.infoPaginacion.actual == page ? "active" : "";
    },
    exportPDF(){
      this.btnPDFStatus = true
      window.open(this.$store.state.services.InvoicingService.exportPDF(), '_blank');
      this.btnPDFStatus = false;
    },
    exportExcel(){
      this.btnExcelStatus = true
      var a = document.createElement('A');
      var filePath = this.$store.state.services.InvoicingService.exportExcel();
      a.href = filePath;
      a.download = filePath.substr(filePath.lastIndexOf('/') + 1);
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      this.btnExcelStatus = false;
    }
  }
};
</script>