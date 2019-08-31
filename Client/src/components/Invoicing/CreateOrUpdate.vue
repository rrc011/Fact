<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button
          size="small"
          @click="$router.push(`/invoicing/page/1`)"
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
          <el-select v-model="form.PersonId" placeholder="Select" required>
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
            <el-button type="primary" icon="el-icon-sell" @click="onSubmit()">Vender</el-button>
        </el-form-item>
      </el-form>

      <hr/>

      <el-table v-loading="loading" :data="tableDataAdded" style="width: 100%" show-summary>
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
                <h5 class="modal-title" id="exampleModalLabel">Buscar productos</h5>
            </div>
            <div class="modal-body">
                <el-table
                    :data="tableData"
                    height="250"
                    style="width: 100%">
                    <el-table-column fixed width="100" label="Codigo" prop="productId"></el-table-column>
                    <el-table-column width="150" label="Product" prop="name"></el-table-column>
                    <el-table-column width="100" label="Stock" prop="stock"></el-table-column>
                    <el-table-column width="150" label="Precio" prop="price"></el-table-column>
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
      search: null,
      num: 1,
      date: new Date(),
      listPerson:[],
      tableDataAdded: [],
      tableData: [],
      listOrderDetail: [],
      form: {
        Amount: null,
        PersonId: null,
      },
      rules: {
        PersonId: [
          { type: 'array', required: true, message: 'Por favor seleccione un cliente ', trigger: 'change' }
        ]
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
            for (var i = 0; i < this.tableDataAdded.length; i+=1) {
              this.form.Amount += this.tableDataAdded[i].total;
            }
            self.$store.state.services.InvoicingService
              .add(self.form)
              .then(r => {
                if(r.data > 0){
                  for (var i = 0; i < this.tableDataAdded.length; i+=1) {
                    this.listOrderDetail.push({
                      "OrderDetailId": 0,
                      "OrderId": r.data,
                      "ProductId": this.tableDataAdded[i].codigo,
                      "Quantity": this.tableDataAdded[i].cantidad
                    })
                  }
                  self.$store.state.services.InvoicingDetailService
                  .add(this.listOrderDetail)
                  .then(r => {
                    self.loading = false;
                    self.$router.push(`/invoicing/page/1`);
                    self.$message({
                      message: "Se guardo con exito",
                      type: "success"
                    });
                  })
                }
              })
              .catch(r => {
                self.$message({
                  message: `${r}`,
                  type: "error"
                });
              });
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
      var product = this.tableData.filter(x => x.productId == productId)
      var newProduct = {
        "codigo": product[0].productId,
        "nombre": product[0].name,
        "cantidad": this.num,
        "descripcion": product[0].descripcion,
        "precioUnidad": product[0].price,
        "total": (product[0].price * this.num)
      }

      this.tableDataAdded.push(newProduct);
    }
  }
};
</script>