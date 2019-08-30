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
        :model="form"
        status-icon
        :rules="rules"
        ref="form"
        label-width="120px"
      >
        <el-form-item label="Cedula" prop="dni">
          <el-input v-model="form.dni"></el-input>
        </el-form-item>
        <el-form-item label="Nombre" prop="name">
          <el-input v-model="form.name" id="name"></el-input>
        </el-form-item>
        <el-form-item label="Apellido" prop="lastname">
          <el-input v-model="form.lastname"></el-input>
        </el-form-item>
        <el-form-item label="Genero" prop="lastname">
          <el-select v-model="form.gender" placeholder="Genero">
                <el-option
                v-for="item in listGender"
                :key="item.value"
                :label="item.label"
                :value="item.value">
                </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Correo" prop="email">
          <el-input v-model="form.email"></el-input>
        </el-form-item>
        <el-form-item label="Telefono" prop="phone">
          <el-input v-model="form.phone"></el-input>
        </el-form-item>
        <el-form-item label="Direccion" prop="address">
          <el-input type="textarea" :show-word-limit="true" v-model="form.address"></el-input>
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
  name: "ClientCreateOrUpdate",
  data() {
    var checkDNI = (rule, value, callback) => {
        this.$store.state.services.ClientService
                               .ValidateDNI(this.form.PersonId, value)
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
      listGender:[],
      selectValue: null,
      form: {
        dni: null,
        PersonId: 0,
        name: null,
        lastname: null,
        gender: null,
        email: null,
        phone: null,
        address: null,
        deleted: false
      },
      rules: {
        name: [
          {required: true, message: "Debe ingresar un nombre"},
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        dni: [
          {required: true, message: "Debe ingresar una cedula"},
          { validator: checkDNI, trigger: "blur" },
          { min: 11, max: 11, message: "Debe contener entre 11 caracteres", trigger: 'blur' }
        ],
        lastname: [
          {required: true, message: "Debe ingresar un apellido", trigger: 'blur'},
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres", trigger: 'blur' }
        ],
        email: [
          { type: 'email', message: 'Formato incorrecto', trigger: ['blur', 'change'] }
        ],
        gender: [
          {required: true, message: "Debe selecionar un genero", trigger: 'blur'}
        ],
        address: [
          { min: 3, max: 50, message: "Debe contener entre 3 y 250 caracteres", trigger: 'blur' }
        ]
      }
    };
  },
  computed: {
    pageTitle() {
      return this.form.PersonId === 0 ? "Nuevo Cliente" : this.form.name;
    }
  },
  created() {
    let self = this;
    self.get(self.$route.params.id);
    this.loadSelectGender();
  },
  methods: {
    get(id) {
      if (id == undefined) return;

      let self = this;

      self.loading = true;
      self.$store.state.services.ClientService
        .get(id)
        .then(r => {
          self.form.PersonId = r.data.personId;
          self.form.name = r.data.name;
          self.form.lastname = r.data.lastName;
          self.form.dni = r.data.dni;
          self.form.email = r.data.email;
          self.form.address = r.data.addess;
          self.form.phone = r.data.phone;
          self.selectValue = r.data.gender;
          self.form.gender = r.data.genderName;
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
    loadSelectGender(){
        this.listGender = [{
          value: '1',
          label: 'Masculino'
        }, {
          value: '2',
          label: 'Femenino'
        }]
    }
  }
};
</script>