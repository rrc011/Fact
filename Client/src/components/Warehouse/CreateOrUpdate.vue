<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button
          size="small"
          @click="$router.push(`/warehouse/page/1`)"
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
        <el-form-item label="Ubicacion" prop="location">
          <el-input v-model="form.location" id="location"></el-input>
        </el-form-item>
        <el-form-item label="Descripcion" prop="description">
          <el-input type="textarea" :show-word-limit="true" v-model="form.description"></el-input>
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
  name: "WarehouseCreateOrUpdate",
  data() {
    var checkName = (rule, value, callback) => {
        this.$store.state.services.WarehouseService
                               .validateName(this.form.warehouseId, value)
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
      form: {
        warehouseId: 0,
        name: null,
        location: null,
        description: null,
        deleted: false
      },
      rules: {
        name: [
          {required: true, message: "Debe ingresar un nombre"},
          { validator: checkName, trigger: "blur" },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        location: [
          {required: true, message: "Debe ingresar una ubicacion"},
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
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
      return this.form.warehouseId === 0 ? "Nuevo Almacen" : this.form.name;
    }
  },
  created() {
    let self = this;
    self.get(self.$route.params.id);
  },
  methods: {
    get(id) {
      if (id == undefined) return;

      let self = this;

      self.loading = true;
      self.$store.state.services.WarehouseService
        .get(id)
        .then(r => {
          self.form.warehouseId = r.data.id;
          self.form.name = r.data.name;
          self.form.location = r.data.location;
          self.form.description = r.data.description;
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
          if (self.form.warehouseId > 0) {
            self.$store.state.services.WarehouseService
              .update(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/warehouse/page/1`);
                self.$message({
                  message: "Se actualizo el almacen con exito",
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
            self.$store.state.services.WarehouseService
              .add(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/warehouse/page/1`);
                self.$message({
                  message: "Se guardo el alamacen con exito",
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
    }
  }
};
</script>