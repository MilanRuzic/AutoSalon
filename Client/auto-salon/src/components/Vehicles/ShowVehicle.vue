<template>
  <div class="all" v-if="!isSelected">
    <div class="manage-vehicle" v-if="userExists">
      <button>Edit vehicle</button>
      <button @click="deleteVehicle">Delete vehicle</button>
    </div>
    <div class="pictures">
      <div :style="style" class="image"></div>
      <div class="restImages">
        <image-card
          class="sl"
          v-for="picture in slike"
          :key="picture.slikeid"
          :id="picture.slikeid"
          :slika="picture.nazivslike"
        ></image-card>
      </div>
    </div>
    <div class="car">
      <h2>
        {{ proizvodjac }}
        {{ model }}
      </h2>

      <div class="performances">
        <label class="details even">Proizvodjac: {{ proizvodjac }}</label
        ><br />
        <label @click="findModels" class="details">Model: {{ model }}</label>
        <label class="details even">Vrsta goriva: {{ vrstaGoriva }}</label
        ><br />
        <label class="details ">Snaga motora: {{ snagaMotora }}KW</label><br />
        <label class="details even">Kilometraza:{{ kilometraza }}km</label>
        <label class="details ">Cijena: {{ cena }} KM</label>
        <label class="details even ">Opis: {{ opis }}</label
        ><br /><br />
        <div v-if="buyer" class="sold">
          <label>Prodato!</label>
        </div>
      </div>
    </div>
  </div>
  <div v-else><p>LOADING</p></div>
</template>
<script>
import ImageCard from "./ImageCard.vue";

export default {
  components: {
    ImageCard,
  },
  data() {
    return {};
  },
  computed: {
    userExists() {
      return this.$store.getters.getUser;
    },
    isSelected() {
      return this.$store.getters.loading;
    },
    buyer() {
      return this.$store.getters.getSelectedVehicle.kupac;
    },
    style() {
      return {
        backgroundImage:
          "url(" + require("./image/" + this.$store.getters.style) + ")",
      };
    },
    slike() {
      return this.$store.getters.getSelectedVehicle.slikes;
    },
    proizvodjac() {
      return this.$store.getters.getSelectedVehicle.model.proizvodjac
        .nazivproizvodjaca;
    },
    model() {
      return this.$store.getters.getSelectedVehicle.model.nazivmodela;
    },
    modelId() {
      return this.$store.getters.getSelectedVehicle.model.modelid;
    },
    vrstaGoriva() {
      return this.$store.getters.getSelectedVehicle.vrstagoriva
        .nazivvrstegoriva;
    },
    snagaMotora() {
      return this.$store.getters.getSelectedVehicle.snagamotora;
    },
    kilometraza() {
      return this.$store.getters.getSelectedVehicle.kilometraza;
    },
    cena() {
      return this.$store.getters.getSelectedVehicle.cena;
    },
    opis() {
      return this.$store.getters.getSelectedVehicle.opis;
    },
  },
  beforeMount() {
    const id = this.$route.query.voziloid;
    this.$store.dispatch("selectVehicle", { value: id });
  },
  methods: {
    deleteVehicle() {
      this.$store.dispatch("deleteVehicle", {
        value: this.$store.getters.getSelectedVehicle.voziloid,
      });
    },
    findModels() {
      this.$router.push({
        name: "filter-vehicle",
        query: {
          proizvodjac: this.$store.getters.getDefaultFilter.selected,
          model: this.modelId,
          cenaod: this.$store.getters.getDefaultFilter.priceFrom,
          cenado: this.$store.getters.getDefaultFilter.priceTo,
          vrstagoriva: this.$store.getters.getDefaultFilter.fuelTypeId,
          kilometrazaod: this.$store.getters.getDefaultFilter.kilometersFrom,
          kilometrazado: this.$store.getters.getDefaultFilter.kilometersTo,
          snagamotoraod: this.$store.getters.getDefaultFilter.motorPowerFrom,
          snagamotorado: this.$store.getters.getDefaultFilter.motorPowerTo,
          prodata: this.$store.getters.getDefaultFilter.checked,
        },
      });
    },
  },
};
</script>
<style scoped>
h2 {
  color: dimgrey;
  margin-left: 20%;
  font-size: 40px;
  font-family: "Cabin", sans-serif;
}
.pictures {
  display: inline-block;
  margin: 2%;
  width: 40%;
  height: 40rem;
}
.restImages {
  display: flex;
  flex-flow: row wrap;
  align-content: space-around;
}
.image {
  width: 100%;
  height: 25rem;
  background-repeat: no-repeat;
  background-size: 100% 100%;
}
.performances {
  display: flex;
  flex-flow: row wrap;
  width: 70%;
}
.details {
  width: 100%;
  height: 2rem;
  font-size: 20px;
  line-height: 2rem;
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif,
    Cochin, Georgia, Times, "Times New Roman", serif;
}
.car {
  display: inline-block;
  position: absolute;
  width: 50%;
}
.sold {
  width: 100%;
  text-align: center;
  background-color: red;
  color: white;
}
.all {
  display: inline-block;
  width: 100%;
  margin-left: 5%;
  margin-top: 5%;
}
.sl {
  width: 20%;
  height: 5rem;
}
.even {
  background-color: lightsteelblue;
}
.details:hover,
.link:hover {
  border: gray;
  border-radius: 7px;
  background-color: darkgrey;
}
.link {
  text-decoration: none;
  color: black;
  width: 100%;
  height: 2rem;
  font-size: 20px;
  line-height: 2rem;
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif,
    Cochin, Georgia, Times, "Times New Roman", serif;
}
</style>
