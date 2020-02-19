<template>
  <div class="giver">
    <v-card>
      <v-card-title>Givers</v-card-title>
      <v-data-table :headers="headers" :items="givers" :search="search"></v-data-table>
    </v-card>
  </div>
</template>

<script>
export default {
    name: 'giver',
    created(){
        this.$store.dispatch('getGivers');
    },
    computed:{
        givers(){
            return this.$store.state.givers;
        }
    },
    data(){
        return{
            search: '',
            headers: [
                {
                    text: 'First Name',
                    align: 'left',
                    value: 'firstName',
                },
                {
                    text: 'Last Name',
                    align: 'left',
                    value: 'lastName',
                },
            ],
            persons: [{firstName: '', lastName: ''}],
        }
    }
}
</script>