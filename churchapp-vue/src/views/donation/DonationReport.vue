<template>
<v-row>
    <v-col cols="12">
        <h1>Reports</h1>
    </v-col>
    <v-col>
         <datePicker v-bind:dateEntered.sync="fromDate" />
          <datePicker v-bind:dateEntered.sync="toDate" />
    </v-col>
</v-row>
</template>
<script>
export default {
    name: 'donationReport',
    data:() => ({
        fromDate: (new Date()).toISOString(),
        toDate: (new Date()).toISOString(),
    }),

};
</script>