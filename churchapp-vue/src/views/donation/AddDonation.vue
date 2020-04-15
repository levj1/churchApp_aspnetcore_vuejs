<template>
  <v-card outlined>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-card-title>Add Donations</v-card-title>
      <v-card-text>
        <v-row align="center" v-for="(donation, index) in donations" :key="index">
          <v-col cols="3">
            <v-autocomplete
              label="Person Name"
              :rules="personRules"
              v-model="donation.personId"
              :items="people"
              item-text="fullName"
              item-value="id"
              dense
              filled
            ></v-autocomplete>
          </v-col>
          <v-col cols="3">
            <v-autocomplete
              v-model="donation.donationTypeId"
              label="Donation Type"
              :rules="donationTypeRules"
              :items="donationTypes"
              item-text="type"
              item-value="id"
              dense
              filled
            ></v-autocomplete>
          </v-col>
          <v-col cols="3">
            <v-text-field
              type="number"
              label="Amount"
              prefix="$"
              v-model="donation.amount"
              :rules="amountRules"
            ></v-text-field>
          </v-col>
          <v-col cols="3">
            <v-btn
              class="ma-2"
              small
              color="green"
              @click="addLine"
              v-if="index == donations.length - 1"
            >Add</v-btn>
            <v-btn small color="red" @click="removeLine(index)" v-if="donations.length > 1">Remove</v-btn>
          </v-col>
        </v-row>
        <v-row>
          <v-col align="center">
            <v-btn class="ma-2" color="primary" @click="submit">Submit</v-btn>

            <v-btn color="secondary" @click="cancel">Cancel</v-btn>
          </v-col>
        </v-row>
      </v-card-text>
    </v-form>
    <snackBar
      v-if="show"
      v-bind:open.sync="show"
      v-bind:text.sync="message"
      v-bind:color.sync="color"
    ></snackBar>
  </v-card>
</template>
<script>
import snackBar from "../../shared/Snackbar";
import utilityMixin from "../../shared/mixin/util-mixin.js";

const don = [
  {
    personId: "",
    donationTypeId: "",
    amount: 0,
    donationDate: new Date()
  }
];
export default {
  name: "addDonation",
  mixins: [utilityMixin],
  created() {
    this.$store
      .dispatch("giver/getGivers", {
        includeAddress: false,
        includeDonations: false
      })
      .then()
      .catch(err => {
        console.log("An error occured while loading people");
      });

    this.$store
      .dispatch("donationType/getDonationTypes")
      .then()
      .catch(err => {
        console.log("An error occured while loading donation types");
      });
  },
  components: {
    snackBar
  },
  computed: {
    people() {
      return this.$store.state.giver.givers;
    },
    donationTypes() {
      return this.$store.state.donationType.donationTypes;
    }
  },
  data: () => ({
    show: false,
    color: "error",
    message: "",
    valid: true,
    donations: don,
    personRules: [v => !!v || "Person is Required"],
    donationTypeRules: [v => !!v || "Donation Type is Required"],
    amountRules: [
      v => !!v || "Amount is Required",
      v => v > 0 || "Amount must be great than zero"
    ],
  }),
  methods: {
    addLine() {
      if (this.$refs.form.validate()) {
        this.donations.push({
          personId: 0,
          donationTypeId: 0,
          amount: 0,
          donationDate: this.formatDate(new Date())
        });
      }
    },
    removeLine(i) {
      this.donations.splice(i, 1);
    },
    submit() {
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch("donation/createDonations", JSON.stringify(this.donations))
          .then(res => {
            this.donations = don;
            this.$router.push("/donations");
          })
          .catch(err => {
            this.message = "Failed! An error occured while adding donations";
            this.show = true;
            this.color = "error";
          });
      }
    },
    cancel() {
      this.$refs.form.reset();
      this.$router.push("/donations");
    }
  }
};
</script>