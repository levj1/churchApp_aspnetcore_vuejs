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
    <Snackbar :message="snackMessage" :showSnackbar="showSnack" :barColor="snackColor" />
  </v-card>
</template>
<script>
import Snackbar from "../../shared/Snackbar";

function formatDate(d, format) {
  const date = new Date(d);
  let month = (date.getMonth() + 1).toString();
  let day = date.getUTCDate().toString();
  const year = date.getFullYear().toString();
  if (month.length < 2) {
    month = "0" + month;
  }
  if (day.length < 2) {
    day = "0" + day;
  }

  switch (format) {
    case "yyyy-mm-dd":
      return [year, month, day].join("-");

    case "mm/dd/yyyy":
      return [month, day, year].join("/");

    case "mm-dd-yyyy":
      return [month, day, year].join("-");

    default:
      return [year, month, day].join("-");
  }
}
const don = [
  {
    personId: "",
    donationTypeId: "",
    amount: 0,
    donationDate: formatDate(new Date())
  }
];
export default {
  name: "addDonation",
  created() {
    this.$store
      .dispatch("getGivers", {
        includeAddress: false,
        includeDonations: false
      })
      .then()
      .catch(err => {
        console.log("An error occured while loading people");
      });

    this.$store
      .dispatch("getDonationTypes")
      .then()
      .catch(err => {
        console.log("An error occured while loading donation types");
      });
  },
  components: {
    Snackbar
  },
  computed: {
    people() {
      return this.$store.state.givers;
    },
    donationTypes() {
      return this.$store.state.donationTypes;
    }
  },
  data: () => ({
    valid: true,
    donations: don,
    personRules: [v => !!v || "Person is Required"],
    donationTypeRules: [v => !!v || "Donation Type is Required"],
    amountRules: [
      v => !!v || "Amount is Required",
      v => v > 0 || "Amount must be great than zero"
    ],
    snackMessage: "",
    showSnack: false,
    snackColor: "success"
  }),
  methods: {
    addLine() {
      if (this.$refs.form.validate()) {
        this.donations.push({
          personId: 0,
          donationTypeId: 0,
          amount: 0,
          donationDate: formatDate(new Date())
        });
      }
    },
    removeLine(i) {
      this.donations.splice(i, 1);
    },
    submit() {
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch("createDonations", JSON.stringify(this.donations))
          .then(res => {
            this.donations = don;
            this.$router.push("/donations");
          })
          .catch(err => {
            this.snackMessage =
              "Failed! An error occured while adding donations";
            this.showSnack = true;
            this.snackColor = "error";
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