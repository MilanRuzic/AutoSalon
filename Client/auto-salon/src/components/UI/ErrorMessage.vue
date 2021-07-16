<template>
  <div class="error">
    <slot></slot>
  </div>
</template>
<script>
export default {};
</script>
<style>
.error {
  display: block;
  border: solid 1px rgb(173, 1, 1);
  border-radius: 5px;
  background-color: rgb(255, 97, 97);
  width: 60%;
  margin: 0 auto;
  height: 5rem;
  text-align: center;
  line-height: 3rem;
  color: rgb(173, 1, 1);
}
</style>
