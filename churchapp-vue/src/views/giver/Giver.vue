<template>
  <div class="giver">
    <v-card>
      <v-card-title>Givers</v-card-title>
      <v-data-table :headers="headers" :items="givers" :search="search"></v-data-table>
    </v-card>
  </div>
</template>

<script>
// import store from '../store/index'; 

export default {
    name: 'giver',
    created(){
        this.$store.dispatch('getGivers');
        if(this.$store.state.currentUser == null){
            this.$router.push('/');
        }
    },
    // beforeRouteUpdate: (to, from, next) => {
    //     if(this.$store.state.currentUser !== null){
    //         next(true);
    //     }else{
    //         next(false);
    //     }
    // },
    computed:{
        givers(){
            return this.$store.state.givers;
        },
        isLoggedIn(){
            return this.$store.state.currentUser !== null
        },
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