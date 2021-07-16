<template>
  <div>
    <slot></slot>
  </div>
</template>
<style scoped>
div {
  /**/

  background-color: #ebebeb;
  box-shadow: 0px 0px 1px #ccc;
  float: left;
  overflow: hidden;
  padding: 0px;
  position: relative;
  width: 208px;
  cursor: pointer;
  margin: 20px 10px 0px 10px;
  border-radius: 0px 0px 10px 10px;
  transition: box-shadow 0.2s ease-in-out;
}
div:hover {
  border-radius: 3px 3px 12px 12px;
  box-shadow: 2px 2px 4px grey;
  /* border-top: 3px solid red;*/
}
</style>
