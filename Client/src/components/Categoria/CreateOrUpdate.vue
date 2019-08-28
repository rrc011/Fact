<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button
          size="small"
          @click="$router.push(`/categoria/page/1`)"
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
        <el-form-item label="Nombre" prop="Nombre">
          <el-input v-model="form.Nombre" id="nombre"></el-input>
        </el-form-item>
        <el-form-item label="Descripcion" prop="Descripcion">
          <el-input type="textarea" :show-word-limit="true" v-model="form.Descripcion"></el-input>
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
  name: "CategoriaCreateOrUpdate",
  data() {
    var checkName = (rule, value, callback) => {
        this.$store.state.services.categoriaService
                               .validateName(this.form.CategoriaId, value)
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
        CategoriaId: 0,
        Nombre: null,
        Descripcion: null,
        EstadoReg: true
      },
      rules: {
        Nombre: [
          {required: true, message: "Debe ingresar un documento de identidad"},
          { validator: checkName, trigger: "blur" },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        Descripcion: [
          { required: true, message: "Debe ingresar un nombre", trigger: 'blur' },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ]
      }
    };
  },
  computed: {
    pageTitle() {
      return this.form.CategoriaId === 0 ? "Nuevo Categoria" : this.form.Nombre;
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
      self.$store.state.services.categoriaService
        .get(id)
        .then(r => {
          self.form.CategoriaId = r.data.categoriaId;
          self.form.Nombre = r.data.nombre;
          self.form.Descripcion = r.data.descripcion;
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
      self.$refs["form"].validate(valid => {
        if (valid) {
          self.loading = true;
          if (self.form.CategoriaId > 0) {
            self.$store.state.services.categoriaService
              .update(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/categoria/page/1`);
                self.$message({
                  message: "Se actualizo la categoria con exito",
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
            self.$store.state.services.categoriaService
              .add(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/categoria/page/1`);
                self.$message({
                  message: "Se guardo la categoria con exito",
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