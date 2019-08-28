<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button
          size="small"
          @click="$router.push(`/persona/page/1/${$route.params.tipo}`)"
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
        <el-form-item label="DNI" prop="Cedula">
          <el-input v-model="form.Cedula" id="cedula" :disabled="dni"></el-input>
        </el-form-item>
        <el-form-item label="Nombres" prop="Nombres">
          <el-input v-model="form.Nombres"></el-input>
        </el-form-item>
        <el-form-item label="Apellidos" prop="Apellidos">
          <el-input v-model="form.Apellidos"></el-input>
        </el-form-item>
        <el-form-item label="Correo" prop="Correo">
          <el-input v-model="form.Correo"></el-input>
        </el-form-item>
        <el-form-item label="Telefono" prop="Telefono">
          <el-input v-model="form.Telefono" id="phone"></el-input>
        </el-form-item>
        <el-form-item label="Direccion" prop="Direccion">
          <el-input type="textarea" :show-word-limit="true" v-model="form.Direccion"></el-input>
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
  name: "PersonaCreateOrUpdate",
  data() {
    var checkDNI = (rule, value, callback) => {
        this.$store.state.services.personaService
                               .validateDNI(value)
                               .then(r => {
                                  var c = document.getElementById("cedula").disabled;
                                  if (r.data && !c) {
                                    callback(new Error("Esta documento ya se encuentra registrado"));
                                  } else {
                                    callback();
                                  }
        });
    };
    return {
      loading: false,
      dni: false,
      form: {
        PersonaId: 0,
        Nombres: null,
        Apellidos: null,
        Correo: null,
        Telefono: null,
        Direccion: null,
        EstadoReg: false,
        Tipo: 0,
        Cedula: null
      },
      rules: {
        Cedula: [
          {required: true, message: "Debe ingresar un documento de identidad"},
          { validator: checkDNI, trigger: "blur" },
          { max: 11, message: "Debe contener 11 caracteres" }
        ],
        Nombres: [
          { required: true, message: "Debe ingresar un nombre", trigger: 'blur' },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        Apellidos: [
          { required: true, message: "Debe ingresar un apellido", trigger: 'blur' },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        Correo: [
          {
            type: "email",
            message: "Por favor ingrese un email correcto",
            trigger: ["blur", "change"]
          }
        ]
      }
    };
  },
  computed: {
    pageTitle() {
      return this.form.PersonaId === 0 ? "Nuevo Persona" : this.form.Nombres;
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
      self.$store.state.services.personaService
        .get(id)
        .then(r => {
          self.form.PersonaId = r.data.personaId;
          self.form.Nombres = r.data.nombres;
          self.form.Apellidos = r.data.apellidos;
          self.form.Correo = r.data.correo;
          self.form.Direccion = r.data.direccion;
          self.form.Telefono = r.data.telefono;
          self.form.Tipo = r.data.tipo;
          self.form.EstadoReg = r.data.estadoReg;
          self.form.Cedula = r.data.cedula;
          self.loading = false;
          self.dni = self.form.Cedula != "" ? true : false;
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
          this.form.Tipo = this.form.Tipo > 0 ? this.form.Tipo : parseInt(this.$route.params.tipo);
          self.loading = true;
          if (self.form.PersonaId > 0) {
            self.$store.state.services.personaService
              .update(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/persona/page/1/${$route.params.tipo}`);
                self.$message({
                  message: "Se actualizo la persona con exito",
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
            self.$store.state.services.personaService
              .add(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push(`/persona/page/1/${$route.params.tipo}`);
                self.$message({
                  message: "Se guardo la persona con exito",
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