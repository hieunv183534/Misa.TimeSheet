<template>
    <div id="app">
        <button @click="scrollIntoView(35)">scroll into view</button>
        <div id="container" ref="container" style="height: 200px; overflow-y: scroll">
            <p v-for="n in 100" :key="n" :id="`num-${n}`">{{n}}</p>
        </div>
    </div>
</template>
<script>
export default {
  name: "App",
  methods: {
    scrollIntoView(x) {
          const el = this.$el.querySelector(`#num-${x}`);
      if (el) {
        el.scrollIntoView();
      }
    }
  }
};
</script>