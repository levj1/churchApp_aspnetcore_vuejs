<template>
  <div class="donation">
    <div class="text-right pa-2" >
        <v-btn color="secondary" @click="addDonation">Add Donation</v-btn>
    </div>

    <v-card>
      <v-card-title>
        Donations
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <v-data-table :headers="headers" :items="donations" :search="search">
        <template v-slot:item.donationDate="{ item }">
          <span>{{formatDate(item.donationDate, 'mm-dd-yyyy')}}</span>
          <!-- <v-icon small @click="deletePerson(item)">mdi-delete</v-icon> -->
        </template>
        <template v-slot:item.action="{ item }">
          <v-icon class="mr-2" @click="editDonation(item)">mdi-pencil</v-icon>
          <v-icon @click="deleteDonation(item)">mdi-delete</v-icon>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
export default {
  name: "giver",
  components: {},
  created() {
    this.$store
      .dispatch("getDonations")
      .then(res => {
        this.donations = res.data;
      })
      .catch(err => {
        console.log("error while getter donations");
      });
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.currentUser !== null;
    }
  },
  data() {
    return {
      search: "",
      donations: [],
      headers: [
        {
          text: "Full Name",
          align: "left",
          value: "person.fullName"
        },
        {
          text: "Amount",
          align: "left",
          value: "amount"
        },
        {
          text: "Date",
          align: "left",
          value: "donationDate"
        },
        {
          text: "Type",
          align: "left",
          value: "donationType.type"
        },

        { text: "Actions", value: "action", sortable: false }
      ],
      persons: [{ firstName: "", lastName: "", middleName: "", addresses: [] }]
    };
  },
  methods: {
    editDonation(item) {
      this.$router.push({ name: "EditDonation", params: { id: item.id } });
    },
    addDonation() {
      this.$router.push({ name: "AddDonation" });
    },
    deleteDonation(item) {
      let resp = confirm("Are you sure you want to delete this donation");
      if (resp) {
        this.$store
          .dispatch("deleteDonation", item.id)
          .then(res => {
            var index = this.donations.findIndex(x => x.id == item.id);
            this.donations.splice(index, 1);

            alert("Successfully delete donation for that person.");
          })
          .catch(err => {
            console.log(err);
            alert("Failed to delete the donation for this person.");
          });
      }
    },
    formatDate(d, format) {
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
  }
};
</script>