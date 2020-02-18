<template>
    <div class="giver">
        <h1>Givers</h1>
    </div>
</template>

<script>
export default {
    name: 'giver',
    created(){
        this.$store.dispatch('getGivers');
    },
    computed:{
        // givers(){
        //     return this.$store.state.givers;
        // }
    },
    data(){
        return{
            count: 0,
        }
    }
}
</script>