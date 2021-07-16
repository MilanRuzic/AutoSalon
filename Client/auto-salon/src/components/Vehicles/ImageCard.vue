<template>
  <div :style="style" class="container"></div>
</template>

<script>
export default {
  props: ["id", "slika"],
  computed: {
    style() {
      return {
        backgroundImage: "url(" + require("./image/" + this.slika) + ")",
      };
    },
  },
};
</script>

<style>
.container {
  width: 100%;
  height: 25rem;
  background-repeat: no-repeat;
  background-size: 100% 100%;
  margin: 20px 10px 0px 10px;
}
.container:hover {
  box-shadow: 2px 2px 4px grey;
}
</style>
