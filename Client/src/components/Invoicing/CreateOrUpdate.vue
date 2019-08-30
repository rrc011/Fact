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

      <hr/>

      <el-table :data="tableDataAdded" style="width: 100%" show-summary>
        <el-table-column prop="codigo" label="Codigo">
        </el-table-column>
        <el-table-column prop="nombre" label="Nombre">
        </el-table-column>
        <el-table-column prop="descripcion" label="Descripcion">
        </el-table-column>
        <el-table-column prop="cantidad" label="Cant.">
        </el-table-column>
        <el-table-column prop="precioUnidad" label="PRECIO UNIT.">
        </el-table-column>
        <el-table-column prop="total" label="Total">
        </el-table-column>
      </el-table>
    </el-card>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                  <div class="col-lg-6">
                    <h5 class="modal-title" id="exampleModalLabel">Buscar productos</h5>
                  </div>
                  <div class="col-lg-6">
                    <el-autocomplete
                      v-model="state"
                      :fetch-suggestions="querySearchAsync"
                      placeholder="Buscar..."
                      @select="handleSelect(state)">
                    </el-autocomplete>
                  </div>
                </div>
            </div>
            <div class="modal-body">
                <el-table
                    :data="newtableData"
                    style="width: 100%">
                    <el-table-column width="100" label="Codigo" prop="productId"></el-table-column>
                    <el-table-column width="150" label="Product" prop="name"></el-table-column>
                    <el-table-column width="100" label="Stock" prop="stock"></el-table-column>
                    <el-table-column width="150" label="Precio" prop="price"></el-table-column>
                    <el-table-column width="150" label="Cant.">
                        <template slot-scope="scope"> 
                          <div>
                            <el-input-number size="mini" :min="1" :max="scope.row.stock" v-model="num"></el-input-number>
                            <!-- <input type="number" id="{{scope.row.productId}} + '' + {{scope.row.name}}"/> -->
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column>
                        <template slot-scope="scope">
                            <el-button type="success" @click="addInvoice(scope.row.productId)" icon="el-icon-sold-out" circle>
                            </el-button>
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
      num: [],
      search: null,
      date: new Date(),
      listPerson:[],
      tableDataAdded: [],
      tableData: [],
      newtableData: [],
      form: {
        Amount: null,
        PersonId: null,
      },
      state: [],
      timeout:  null
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
        this.$store.state.services.ProductService
        .getAllToSell()
        .then(r => {
          this.tableData = r.data
        })
        .catch(r => {
          this.$message({
            message: `${r}`,
            type: "error"
          });
        });
    },
    addInvoice(productId){
      var product = this.tableData.items.filter(x => x.productId == productId)
      console.log(product)
      var newProduct = {
        "codigo": product[0].productId,
        "nombre": product[0].name,
        "cantidad": this.num4,
        "descripcion": product[0].descripcion,
        "precioUnidad": product[0].price,
        "total": (product[0].price * this.num4)
      }

      this.tableDataAdded.push(newProduct);
      //console.log(this.tableDataAdded)
    },
     querySearchAsync(queryString, cb) {
        var links = this.tableData;
        var results = queryString ? links.filter(this.createFilter(queryString)) : links;

        clearTimeout(this.timeout);
        this.timeout = setTimeout(() => {
          cb(results);
        }, 3000 * Math.random());
      },
      createFilter(queryString) {
        return (link) => {
          return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
        };
      },
    handleSelect(item) {
        this.newtableData = this.tableData.items.filter(x => x.Name == item);
    }
  }
};
</script>