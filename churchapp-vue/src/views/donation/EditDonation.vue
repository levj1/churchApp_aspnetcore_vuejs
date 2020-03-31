<template>
  <v-form ref="form" v-model="valid" lazy-validation>
    <v-card>
      <v-card-title>Edit Donation</v-card-title>
      <v-card-text>
        <v-row align="center">
          <v-col cols="3">
            <v-text-field disabled v-model="donation.person.fullName" label="Name" outlined shaped></v-text-field>
          </v-col>
          <v-col cols="3">
            <v-autocomplete
              v-model="donation.donationTypeId"
              label="Donation Type"
              :rules="donationTypeRules"
              :items="getDonationTypes"
              item-text="type"
              item-value="id"
              outlined
              shaped
            ></v-autocomplete>
          </v-col>
          <v-col cols="3">
            <v-text-field
              type="number"
              v-model="donation.amount"
              label="Amount"
              prefix="$"
              outlined
              shaped
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col align="center">
            <v-btn color="primary">Edit</v-btn>
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
  </v-form>
</template>
<script>
export default {
  name: "EditDonation",
  created() {
    this.$store
      .dispatch("getDonationTypes")
      .then().catch();

    const { id } = this;
    if (+id > 0) {
      this.$store
        .dispatch("getADonation", id, {
          includeAddress: false,
          includeDonations: false
        }).then(res => {
          if (res && res.data) {
            this.donation = res.data;
            if (res.data.person) {
              this.people.push(res.data.person);
            }
          }
        }).catch();
    }
  },
  props: {
    id: {
      type: [String, Number],
      validator(value) {
        return Number.isInteger(Number(value));
      }
    }
  },
  computed: {
    getDonationTypes() {
      return this.$store.state.donationTypes;
    }
  },
  data() {
    return {
      donation: {
        person: { fullName: "" },
        donationTypeId: 0
      },
      valid: true,
      personRules: [v => !!v || "Person is Required"],
      donationTypeRules: [v => !!v || "Donation Type is Required"],
      amountRules: [
        v => !!v || "Amount is Required",
        v => v > 0 || "Amount must be great than zero"
      ]
    };
  }
};
</script>