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

      <el-row>
        <el-col :span="24"><div class="grid-content bg-purple-dark">
            <el-tag type="info">Factura Nº: {{orderId}}</el-tag> <el-tag type="info">Cliente: {{client}}</el-tag>    
        </div></el-col>
      </el-row>

      <hr/>

      <el-table v-loading="loading" :data="listOrderDetail" style="width: 100%" show-summary>
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

  </div>
</template>

<script>
export default {
  name: "InvoicingDetail",
  data() {
    return {
      loading: false,
      orderId: null,
      client: null,
      listOrderDetail: [],
    };
  },
  computed: {
    pageTitle() {
      return "Detalle Venta";
    }
  },
  created() {
      this.loadProducts(this.$route.params.id);
      this.loadInfo(this.$route.params.id);
  },
  methods: {
    loadProducts(orderId){
        this.loading = true;
        this.$store.state.services.InvoicingDetailService
        .getAll(orderId)
        .then(r => {
          var product = r.data

          for (var i = 0; i < product.length; i+=1) {
                    
                    var newProduct = {
                            "codigo": product[i].productId,
                            "nombre": product[i].name,
                            "cantidad": 1,
                            "descripcion": product[i].descripcion,
                            "precioUnidad": product[i].price,
                            "total": (product[i].price)
                        }

                        this.listOrderDetail.push(newProduct);
          }
          this.loading = false;
        })
        .catch(r => {
          this.$message({
            message: `${r}`,
            type: "error"
          });
        });
    },
    loadInfo(orderId){
        this.$store.state.services.InvoicingService
        .get(orderId)
        .then(r => {
          this.orderId = r.data.orderId;
          this.client = r.data.personName;
        })
        .catch(r => {
          this.$message({
            message: `${r}`,
            type: "error"
          });
        });
    }
  }
};
</script>