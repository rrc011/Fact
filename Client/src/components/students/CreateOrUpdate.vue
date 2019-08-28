<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{pageTitle}}</span>
        <el-button size="small" @click="$router.push('/students/page/1')" icon="el-icon-back" style="float: right; margin-top: -0.5%;" type="primary">
          Atras
        </el-button>
      </div>
      <el-form v-loading="loading" :model="form" :rules="rules" ref="form" label-width="120px">
        <el-form-item label="Nombres" prop="Name">
          <el-input v-model="form.Name"></el-input>
        </el-form-item>
        <el-form-item label="Apellidos" prop="LastName">
          <el-input v-model="form.LastName"></el-input>
        </el-form-item>
        <el-form-item label="Acerca de mi" prop="Bio">
          <el-input type="textarea" v-model="form.Bio"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit">Guardar</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
export default {
  name: "StudentCreateOrUpdate",
  data() {
    return {
      loading: false,
      form: {
        StudentId: 0,
        Name: null,
        LastName: null,
        Bio: null
      },
      rules: {
        Name: [
          { required: true, message: "Debe ingresar un nombre" },
          { min: 3, max: 15, message: "Debe contener entre 3 y 15 caracteres" }
        ],
        LastName: [
          { required: true, message: "Debe ingresar un apellido" },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres" }
        ],
        Bio: [
          { required: true, message: "Debe ingresar una biografia" },
          { min: 3, max: 50, message: "Debe contener entre 3 y 50 caracteres" }
        ]
      }
    };
  },
  computed: {
    pageTitle() {
      return this.form.StudentId === 0 ? "Nuevo Estudiante" : this.form.Name;
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
      self.$store.state.services.studentService
        .get(id)
        .then(r => {
          console.log(r.data);
          self.form.StudentId = r.data.studentId;
          self.form.Name = r.data.name;
          self.form.LastName = r.data.lastName;
          self.form.Bio = r.data.bio;
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
          if (self.form.StudentId > 0) {
            self.$store.state.services.studentService
              .update(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push("/students/page/1");
                self.$message({
                  message: "Se actualizo el estudiante con exito",
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
            self.$store.state.services.studentService
              .add(self.form)
              .then(r => {
                self.loading = false;
                self.$router.push("/students/page/1");
                self.$message({
                  message: "Se guardo el estudiante con exito",
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