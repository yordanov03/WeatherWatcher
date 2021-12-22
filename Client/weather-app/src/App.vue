<template>
  <div id="app">
    <Header/>
    <Weather />
  </div>
</template>

<script>
import Weather from './components/Weather.vue'
import Header from './components/Header.vue'

export default {
  name: 'App',
  components: {
    Weather,
    Header
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
