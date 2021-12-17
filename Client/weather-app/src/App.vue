<template>
  <div id="app">
    <Header/>
    <Weather />
  </div>
</template>

<script>
import Header from './components/Header.vue'
import Weather from './components/Weather.vue'

export default {
  name: 'App',
  components: {
    Header,
    Weather,
  },
}
</script>

<style scoped>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
