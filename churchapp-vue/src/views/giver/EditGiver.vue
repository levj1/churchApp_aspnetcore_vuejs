<template>
  <v-row justify="center">
    <v-card flat>
      <v-card-title>
        <span class="headline">Person</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <v-form ref="form" v-model="valid" lazy-validation v-if="person">
            <v-row>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="person.firstName"
                  label="First name*"
                  :rules="firstNameRules"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  label="Middle name"
                  v-model="person.middleName"
                  hint="example of helper text only on focus"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  label="Last name*"
                  v-model="person.lastName"
                  :rules="lastNameRules"
                  hint="example of persistent helper text"
                  persistent-hint
                  required
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col
                cols="12"
                sm="12"
                md="12"
                v-if="person.addresses && person.addresses.length == 0"
              >
                <v-btn @click="addNewAddress">Add New Address</v-btn>
              </v-col>
              <v-row v-for="address in person.addresses" :key="address.Id">
                <v-col cols="12" md="12" align="right">
                  <v-btn color="error" small @click="deleteAddress(address.id)">Delete Address</v-btn>
                </v-col>
                <v-col cols="12" sm="12" md="12">
                  <v-text-field
                    label="Address Line 1"
                    :rules="streetLine1Rules"
                    v-model="address.streetLine1"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="12" md="12">
                  <v-text-field label="Address Line 2" v-model="address.streetLine2"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field label="City" :rules="cityRules" v-model="address.city"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field label="State" :rules="stateRules" v-model="address.state"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field label="Zipcode" :rules="zipcodeRules" v-model="address.zipcode"></v-text-field>
                </v-col>
              </v-row>

              <v-col cols="12" sm="12" align="center">
                <v-btn class="ma-2" color="primary" @click="addOrEdit()">
                  <span v-if="id > 0">Edit</span>
                  <span v-if="id == 0">Create</span>
                </v-btn>
                <v-btn color="secondary" :to="{name: 'Giver'}">Cancel</v-btn>
              </v-col>
            </v-row>
          </v-form>
        </v-container>
        <small>*indicates required field</small>
      </v-card-text>
    </v-card>
    <snackBar
      v-if="show"
      v-bind:open.sync="show"
      v-bind:text.sync="message"
      v-bind:color.sync="color"
    ></snackBar>
  </v-row>
</template>

<script>
import snackBar from "../../shared/Snackbar";

export default {
  name: "GiverTemplate",
  components: {
    snackBar
  },
  async created() {
    const { id } = this.$route.params;
    if (id > 0) {
      this.id = id;
      await this.$store
        .dispatch("giver/getGiver", {
          id: id,
          includeAddress: false,
          includeDonations: false
        })
        .then(res => {
          if (res && res.data) {
            this.person = res.data;
          }
        })
        .catch();
    } else {
      this.person = {
        id: 0,
        firstName: "",
        middleName: "",
        lastName: "",
        addresses: []
      };
    }
  },
  data: () => ({
    show: false,
    message: "",
    color: "error",
    valid: true,
    id: 0,
    address: true,
    person: {},
    firstNameRules: [v => !!v || "First Name is required"],
    lastNameRules: [v => !!v || "Middle Name is required"],
    streetLine1Rules: [v => !!v || "Street Line 1 is Required"],
    cityRules: [v => !!v || "City is Required"],
    stateRules: [v => !!v || "State is Required"],
    zipcodeRules: [v => !!v || "Zipcode is Required"]
  }),
  methods: {
    async addOrEdit() {
      if (this.$refs.form.validate()) {
        if (this.id == 0) {
          this.$store
            .dispatch("giver/createGiver", JSON.stringify(this.person))
            .then(res => {
              this.message = "You have succesfully added this person";
              this.color = "success";
              this.show = true;
              this.$router.push("/giver");
            })
            .catch(err => {
              this.popSnackMessage('An error occured"', true, "error");
            });
        } else {
          this.$store
            .dispatch("giver/editGiver", JSON.stringify(this.person))
            .then(res => {
              this.popSnackMessage(
                "Change successfully saved",
                true,
                "success"
              );
            })
            .catch(err => {
              this.popSnackMessage("An error occured", true, "error");
            });

          await this.person.addresses.forEach(a => {
            if (a.id > 0) {
              var payload = {
                personId: this.person.id,
                address: {
                  id: a.id,
                  streetLine1: a.streetLine1,
                  streetLine2: a.streetLine2,
                  city: a.city,
                  state: a.state,
                  zipcode: a.zipcode
                }
              };
              this.$store
                .dispatch("address/editAddressForPerson", payload)
                .then(res => {
                  this.popSnackMessage(
                    "Changed successfully saved",
                    true,
                    "success"
                  );
                })
                .catch(err => {
                  this.popSnackMessage(
                    "An error occured while adding new address",
                    true,
                    "error"
                  );
                });
            } else {
              this.$store
                .dispatch("address/addAddressForPerson", {
                  personId: this.person.id,
                  address: {
                    id: a.id,
                    streetLine1: a.streetLine1,
                    streetLine2: a.streetLine2,
                    city: a.city,
                    state: a.state,
                    zipcode: a.zipcode,
                    addressTypeId: 1,
                    personId: this.person.id
                  }
                })
                .then(res => {
                  this.popSnackMessage(
                    "Changed successfully saved",
                    true,
                    "success"
                  );
                })
                .catch(err => {
                  this.popSnackMessage(
                    "An error occured while adding new address",
                    true,
                    "error"
                  );
                });
            }
          });
        }
      }
    },
    addNewAddress() {
      this.person.addresses.push({
        id: 0,
        addressTypeId: 1,
        streetLine1: "",
        streetLine2: "",
        city: "",
        state: "",
        zipcode: "",
        personId: this.person.id
      });
    },
    deleteAddress(addressId) {
      if (addressId > 0) {
        if (confirm("Are you sure you want to delete this address?")) {
          this.$store
            .dispatch("address/deleteAddress", {
              addressId: addressId,
              personId: this.person.id
            })
            .then(res => {
              this.popSnackMessage(
                "Successfully deleting address",
                true,
                "success"
              );
              var index = this.person.addresses.findIndex(
                x => x.id == addressId
              );
              this.person.addresses.splice(index, 1);
            })
            .catch(err => {
              this.popSnackMessage(
                "An error occured while deleting address",
                true,
                "error"
              );
            });
        }
      }
    },
    popSnackMessage(message, show, color = "success") {
      this.message = message;
      this.color = color;
      this.show = true;
    }
  }
};
</script>