<template>
  <v-row>
    <v-col cols="12" md="12">
      <div class="donation">
        <div class="text-right pa-2">
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
            </template>
            <template v-slot:item.action="{ item }">
              <v-icon class="mr-2" @click="editDonation(item)">mdi-pencil</v-icon>
              <v-icon @click="deleteDonation(item)">mdi-delete</v-icon>
            </template>
          </v-data-table>
          <snackBar
            v-if="show"
            v-bind:open.sync="show"
            v-bind:text.sync="message"
            v-bind:color.sync="color"
          ></snackBar>

          <dialogMessage
            v-if="showDialog"
            v-bind:dialog.sync="showDialog"
            v-bind:message.sync="alertMessage"
            v-on:dialogConfirmationEvent="confirmEvent"
          ></dialogMessage>
        </v-card>
      </div>
    </v-col>
    <v-col cols="12" md="12">
      <div id="chart" align="center">
        <apexchart type="pie" width="580" :options="chartOptions" :series="sumOfTypes"></apexchart>
      </div>
    </v-col>
  </v-row>
</template>

<script>
import VueApexCharts from "vue-apexcharts";
import utilityMixin from "../../shared/mixin/util-mixin.js";
import snackBar from "../../shared/Snackbar";
import dialogMessage from "../../shared/DialogMessage";

export default {
  name: "giver",
  components: {
    apexchart: VueApexCharts,
    snackBar,
    dialogMessage
  },
  mixins: [utilityMixin],
  created() {
      this.$store
        .dispatch("donation/getDonations")
        .then(res => {
          this.donations = res.data;
          this.sortDataForGraph();
        })
        .catch(err => {
          console.log("error while getter donations");
        });
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.account.currentUser !== null;
    },
    getTypes() {
      return ["Tithe", "Offering", "Pledge", "ReliefFund"];
    }
  },
  data() {
    return {
      donationToDelete: null,
      show: false,
      showDialog: false,
      alertMessage: "",
      color: "error",
      message: "",
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
      persons: [{ firstName: "", lastName: "", middleName: "", addresses: [] }],
      series: [123, 3, 323, 56],
      chartOptions: {
        chart: {
          width: 380,
          type: "pie"
        },
        labels: ["Tithe", "Offering", "Pledge", "ReliefFund"],
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200
              },
              legend: {
                position: "bottom"
              }
            }
          }
        ]
      },
      types: ["Tithe", "Offering", "Pledge", "ReliefFund"],
      sumOfTypes: []
    };
  },
  methods: {
    confirmEvent(val) {
      console.log(this.donationToDelete);
      if (val && this.donationToDelete) {
        this.$store
          .dispatch("donation/deleteDonation", this.donationToDelete.id)
          .then(res => {
            var index = this.donations.findIndex(
              x => x.id == this.donationToDelete.id
            );
            this.donations.splice(index, 1);

            this.message = "Successfully deleted donation for that person.";
            this.color = "success";
            this.show = true;
            this.donationToDelete = null;
          })
          .catch(err => {
            console.log(err);
            this.message = "Failed to delete the donation for this person.";
            this.color = "error";
            this.show = true;
          });
      }
    },
    editDonation(item) {
      this.$router.push({ name: "EditDonation", params: { id: item.id } });
    },
    addDonation() {
      this.$router.push({ name: "AddDonation" });
    },
    deleteDonation(item) {
      this.donationToDelete = item;

      // Confirm deletion by showing alert message
      this.showDialog = true;
      this.alertMessage = "Are you sure you want to delete this donation";
    },
    sortDataForGraph() {
      console.log(this.donations);
      this.types.forEach(type => {
        let sum = 0;
        this.donations.forEach(donation => {
          if (donation.donationType.type === type) {
            sum += donation.amount;
          }
        });
        this.sumOfTypes.push(sum);
      });
    }
  }
};
</script>
<style scoped>
.chart {
  position: relative;
  top: 300px;
}
</style>