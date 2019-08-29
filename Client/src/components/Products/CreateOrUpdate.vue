<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button
          size="small"
          @click="$router.push(`/product/page/1`)"
          icon="el-icon-back"
          style="float: right; margin-top: -0.5%;"
          type="primary"
        >Atras</el-button>
      </div>
      <el-form
        v-loading="loading"
        :model="form"
        status-icon
        :rules="rules"
        ref="form"
        label-width="120px"
      >
        <el-form-item label="Nombre" prop="name">
          <el-input v-model="form.name" id="name"></el-input>
        </el-form-item>
        <el-form-item label="Precio" prop="price">
          <el-input-number v-model="form.price" :min="1"></el-input-number>
        </el-form-item>
        <el-form-item label="Stock" prop="stock">
          <el-input-number v-model="form.stock" :min="1"></el-input-number>
        </el-form-item>
        <el-form-item label="Almacen" prop="warehouseId">
            <el-select v-model="form.warehouseId" placeholder="Almacen">
                <el-option
                v-for="item in listWarehouse"
                :key="item.value"
                :label="item.text"
                :value="item.value">
                </el-option>
            </el-select>         
        </el-form-item>
        <el-form-item label="Descripcion" prop="descripcion">
          <el-input type="textarea" :show-word-limit="true" v-model="form.descripcion"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" :loading="loading" @click="onSubmit">Guardar</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
export default {
  name: "ProductCreateOrUpdate",
  data() {
    var checkName = (rule, value, callback) => {
        this.$store.state.services.ProductService
                               .validateName(this.form.productId, value)
                               .then(r => {
                                  if (r.data) {
                                    callback(new Error("Esta nombre ya esta ocupado"));
                                  } else {
                                    callback();
                                  }
        });
    };
    return {
      loading: false,
      listWarehouse:[],
      selectValue: null,
      form: {
        productId: 0,
        name: null,
        price: 0,
        stock: 0,
        warehouseId: null,
        descripcion: null,
        deleted: false
      },
      rules: {
        name: [
          {required: true, message: "Debe ingresar un nombre"},
          { validator: checkName, trigger: "blur" },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        price: [
        //   {required: true, message: "Debe ingresar un precio", trigger: 'blur'},
          { type: 'number', message: 'debe ser un numero'}
        ],
        stock: [
        //   {required: true, message: "Debe ingresar un stock", trigger: 'blur'},
          { type: 'number', message: 'debe ser un numero'}
        ],
        warehouseId: [
          {required: true, message: "Debe selecionar un almacen", trigger: 'blur'}
        ],
        description: [
          { required: true, message: "Debe ingresar una descripcion", trigger: 'blur' },
          { min: 3, max: 50, message: "Debe contener entre 3 y 250 caracteres", trigger: 'blur' }
        ]
      }
    };
  },
  computed: {
    pageTitle() {
      return this.form.warehouseId === 0 ? "Nuevo Producto" : this.form.name;
    }
  },
  created() {
    let self = this;
    self.get(self.$route.params.id);
    self.loadSelectWarehouse()
  },
  methods: {
    get(id) {
      if (id == undefined) return;

      let self = this;

      self.loading = true;
      self.$store.state.services.ProductService
        .get(id)
        .then(r => {
          self.form.productId = r.data.productId;
          self.form.name = r.data.name;
          self.form.price = r.data.price;
          self.form.stock = r.data.stock;
          self.form.warehouseId = r.data.warehouseName;
          self.form.descripcion = r.data.descripcion;
          self.selectValue = r.data.warehouseId
          console.log(this.form, r.data)
          self.loading = false;
        })
        .catch(r => {
          self.$message({
            message: "Ocurrio un error inesperado",
            type: "error"
          });
        });
    },
    onSubmit() {
      let self = this;
      console.log(this.form)
      self.$refs["form"].validate(valid => {
        if (valid) {
          self.loading = true;
          if (self.form.productId > 0) {
            if(typeof self.form.warehouseId == 'string')
                self.form.warehouseId = this.selectValue
            self.$store.state.services.ProductService
              .update(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/product/page/1`);
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
            self.$store.state.services.ProductService
              .add(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/product/page/1`);
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
    loadSelectWarehouse(){
        let self = this;

        self.$store.state.services.WarehouseService
        .loadSelectWarehouse()
        .then(r => {
          self.listWarehouse = r.data
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