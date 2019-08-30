<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button
          size="small"
          @click="$router.push(`/client/page/1`)"
          icon="el-icon-back"
          style="float: right; margin-top: -0.5%;"
          type="primary"
        >Atras</el-button>
      </div>
      <el-form
        v-loading="loading"
        :inline="true"
        :model="form"
        status-icon
        ref="form"
        label-width="120px"
      >
        <el-form-item label="Cliente" prop="PersonId">
          <el-select v-model="form.PersonId" placeholder="Select">
                <el-option
                v-for="item in listPerson"
                :key="item.value"
                :label="item.text"
                :value="item.value">
                </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="Fecha">
          <el-date-picker
            v-model="date"
            readonly="dateState"
            type="date"
            placeholder="Pick a date">
          </el-date-picker>
        </el-form-item>
        <el-form-item>
          <button type="button" class="btn btn-block btn-info" data-toggle="modal" data-target="#exampleModal">
            <i class="fa fa-search"></i> Buscar productos
          </button>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" icon="el-icon-sell">Vender</el-button>
        </el-form-item>
      </el-form>

      <el-divider></el-divider>

      <el-table :data="tableDataAdded" style="width: 100%" show-summary>
        <el-table-column prop="date" label="Date">
        </el-table-column>
        <el-table-column prop="name" label="Name">
        </el-table-column>
        <el-table-column prop="date" label="Date">
        </el-table-column>
        <el-table-column prop="name" label="Name">
        </el-table-column>
        <el-table-column prop="date" label="Date">
        </el-table-column>
      </el-table>
    </el-card>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Buscar productos</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <el-table
                    :data="tableData"
                    style="width: 100%">
                    <el-table-column label="Codigo" prop="productId"></el-table-column>
                    <el-table-column label="Product" prop="name"></el-table-column>
                    <el-table-column label="Stock" prop="stock"></el-table-column>
                    <el-table-column label="Precio" prop="price"></el-table-column>
                    <el-table-column label="Cant.">
                        <template>
                            <el-input-number :min="1" :max="10">
                            </el-input-number>
                        </template>
                    </el-table-column>
                    <el-table-column>
                        <template>
                            <el-button
                                icon="el-icon-sold-out"
                            ></el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
            </div>
        </div>
    </div>

  </div>
</template>

<script>
export default {
  name: "InvoicingCreateOrUpdate",
  data() {
    return {
      loading: false,
      dateState: true,
      date: new Date(),
      listPerson:[],
      tableDataAdded: [],
      tableData: [],
      form: {
        Amount: null,
        PersonId: null,
      }
    };
  },
  computed: {
    pageTitle() {
      return "Nueva Venta";
    }
  },
  created() {
      this.loadClients();
      this.loadProducts();
  },
  methods: {
    onSubmit() {
      let self = this;
      self.$refs["form"].validate(valid => {
        if (valid) {
          self.loading = true;
          if (self.form.PersonId > 0) {
            if(typeof self.form.gender == 'string')
                self.form.gender = this.selectValue
            console.log(this.form)
            self.$store.state.services.ClientService
              .update(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/client/page/1`);
                self.$message({
                  message: "Se actualizo el producto con exito",
                  type: "success"
                });
              })
              .catch(r => {
                self.$message({
                  message: "Ocurrio un error inesperado",
                  type: "error"
                });
              });
          } else {
            self.$store.state.services.ClientService
              .add(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/client/page/1`);
                self.$message({
                  message: "Se guardo el producto con exito",
                  type: "success"
                });
              })
              .catch(r => {
                self.$message({
                  message: "Ocurrio un error inesperado",
                  type: "error"
                });
              });
          }
        }
      });
    },
    loadClients(){
        let self = this;

        self.$store.state.services.ClientService
        .loadSelectPerson()
        .then(r => {
          self.listPerson = r.data
        })
        .catch(r => {
          self.$message({
            message: "Ocurrio un error inesperado",
            type: "error"
          });
        });
    },
    loadProducts(){
        let self = this;

        self.$store.state.services.ProductService
        .getAll(1)
        .then(r => {
          self.tableData = r.data.items
          console.log(this.tableData)
          console.log(r.data.items)
        })
        .catch(r => {
          self.$message({
            message: "Ocurrio un error inesperado",
            type: "error"
          });
        });
    }
  }
};
</script>