<template>
  <v-container>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="display-2 font-weight-bold mb-3">Welcome to Church App</h1>
      </v-col>
    </v-row>
    <v-row class="text-center" v-if="getUser">
      <v-col>
        <h4>Hi {{getUser.userName}}</h4>
      </v-col>
    </v-row>
    <v-row class="text-center" v-if="getUser === null">
      <v-col v-if="loginError !== ''">
        <div>
          <h3 class="error" width="200px">A error occured</h3>
        </div>
      </v-col>
      <v-col class="mb-5" cols="12">
        <h2 class="headline font-weight-bold mb-3">Log in</h2>

        <v-form ref="form" v-model="valid" lazy-validation>
          <v-text-field v-model="user.username" label="User Name" required :rules="userNameRules"></v-text-field>

          <v-text-field
            v-model="user.password"
            required
            :rules="passwordRules"
            type="password"
            label="Password"
          ></v-text-field>

          <v-btn class="mr-4" @click="login">Submit</v-btn>
        </v-form>
      </v-col>

      <v-col class="mb-5" cols="12">
        <router-link to="/register">
          <v-btn text small color="error">Register</v-btn>
        </router-link>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "LoginPage",
  // created(){
  //   this.user = this.$store.state.user;
  // },
  computed: {
    getUser() {
      return this.$store.state.currentUser;
    }
  },
  data: () => ({
    valid: true,
    user: {
      username: "",
      password: ""
    },
    userNameRules: [v => !!v || "Username is required"],
    passwordRules: [v => !!v || "Password is required"],
    loginError: ''
  }),
  methods: {
    async login() {
      if (this.$refs.form.validate()) {
        await this.$store
          .dispatch("login", this.user)
          .then(res => {
            console.log("success" + res.data);
            this.loginError = '';
          })
          .catch(e => {
            console.log(e);
            this.loginError = 'An error occured while login';
          });
      }
    }
  }
};
</script>
