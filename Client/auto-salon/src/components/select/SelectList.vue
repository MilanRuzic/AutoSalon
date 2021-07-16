<template>
  <div class="selectDiv">
    <label
      ><b>{{ name }}</b></label
    >
    <slot></slot>
  </div>
</template>
<script>
export default {
  props: ["name"],
};
</script>
<style scoped>
.selectDiv {
  text-align: center;
  line-height: 2rem;
  color: black;
}
</style>
