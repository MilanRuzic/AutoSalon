<template>
  <base-card @click="selectVehicle" class="baseCard">
    <div :style="style" class="image"></div>
    <div class="car">
      <h2>
        {{ model.proizvodjac.nazivproizvodjaca }}<br />
        {{ model.nazivmodela }}
      </h2>
    </div>
    <div class="performances">
      <label class="details"
        >Vrsta goriva: {{ fuelType.nazivvrstegoriva }}</label
      ><br />

      <label class="details">Kilometraza:{{ kilometers }}km</label><br />
      <label class="details">Cijena: {{ price }} KM</label>
      <div v-if="buyer" class="sold">
        <label><b>Prodato!</b></label>
      </div>
    </div>
  </base-card>
</template>
<script>
import BaseCard from "../UI/BaseCard.vue";
export default {
  components: { BaseCard },
  props: [
    "id",
    "fuelType",
    "buyer",
    "manufactor",
    "model",
    "kilometers",
    "motorPower",
    "description",
    "pictures",
    "price",
    "incomingDate",
    "sellingDate",
  ],
  data() {
    return {};
  },
  computed: {
    style() {
      return {
        backgroundImage:
          "url(" + require("./image/" + this.pictures[0].nazivslike) + ")",
      };
    },
  },
  methods: {
    selectVehicle() {
      this.$router.push({
        name: "show-vehicle",
        query: { voziloid: this.id },
      });
    },
  },
};
</script>
<style scoped>
h2 {
  color: orangered;
  font-family: "Cabin", sans-serif;
}
.baseCard {
  margin: 2%;
  width: 20%;
}
.image {
  width: 100%;
  height: 10rem;
  background-repeat: no-repeat;
  background-size: 100% 100%;
}
.performances {
  display: flex;
  flex-flow: row wrap;
}
.details {
  width: 100%;
  border-top: 1px solid grey;
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif,
    Cochin, Georgia, Times, "Times New Roman", serif;
}
.sold {
  width: 100%;
  text-align: center;
  background-color: orangered;
}
</style>
