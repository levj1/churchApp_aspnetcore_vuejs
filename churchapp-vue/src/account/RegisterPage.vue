<template>
  <v-container>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="display-2 font-weight-bold mb-3">Welcome to Church App</h1>
      </v-col>

      <v-col class="mb-5" cols="12">
        <h2 class="headline font-weight-bold mb-3">Register</h2>

        <v-form ref="form" v-model="valid" lazy-validation>
          <v-text-field v-model="firstName" label="First Name" required :rules="firstNameRules"></v-text-field>

          <v-text-field v-model="lastName" label="Last Name" required :rules="lastNameRules"></v-text-field>

          <v-text-field v-model="email" label="Email" required :rules="emailRules"></v-text-field>

          <v-text-field v-model="userName" label="Username" required :rules="userNameRules"></v-text-field>

          <v-text-field v-model="password" type="password" label="Password"></v-text-field>

          <v-btn class="mr-4" @click="register">Register</v-btn>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "RegisterPage",

  data: () => ({
    valid: true,
    firstName: "",
    firstNameRules: [v => !!v || "First Name is required"],
    lastName: "",
    lastNameRules: [v => !!v || "Last Name is required"],
    email: "",
    emailRules: [
      v => !!v || "Email is required",
      v => v.includes("@") || "Email must be valid",
      v => v.includes(".") || "Email must be valid"
    ],
    userName: "",
    userNameRules: [v => !!v || "Username is required"],
    password: "",
    passwordRules: [v => !!v || "Password is required"]
  }),
  methods: {
    register() {
      if (this.$refs.form.validate()) {
        //   const user = '';
        console.log(
          Object.assign({
            FirstName: this.firstName,
            LastName: this.lastName,
            UserName: this.userName,
            Email: this.email,
            Password: this.password
          })
        );

        this.$store.dispatch(
          "registerUser",
          Object.assign({
            firstName: this.firstName,
            lastName: this.lastName,
            userName: this.userName,
            email: this.email,
            password: this.password
          })
        );
      }
    },
    isEmailValid(value) {
      if (value) {
        if (!value.includes("@")) return false;
        if (!value.includes(".")) return false;
        return true;
      }
      return true;
    }
  }
};
</script>
