<template>
  <div class="all">
    <div class="filter">
      <filter-vehicles></filter-vehicles>
    </div>

    <div class="divide" v-if="!isLoading">
      <div class="sorting">
        <label>Sortiraj prema: </label>
        <select @change="sortOption" v-model="selectSorting"
          ><option value="0">--</option
          ><option value="1">Cena silazno</option
          ><option value="2">Cena uzlazno</option
          ><option value="3">Datumu silazno</option
          ><option value="4">Datumu uzlazno</option></select
        >
      </div>
      <ul v-if="!badFilter">
        <vehicle
          v-for="vehicle in this.hasVehicles"
          :key="vehicle.voziloid"
          :id="vehicle.voziloid"
          :fuelType="vehicle.vrstagoriva"
          :buyer="vehicle.kupac"
          :manufactor="vehicle.proizvodjac"
          :model="vehicle.model"
          :kilometers="vehicle.kilometraza"
          :motorPower="vehicle.snagamotora"
          :description="vehicle.opis"
          :pictures="vehicle.slikes"
          :price="vehicle.cena"
          :incomingDate="vehicle.datumdolaska"
          :sellingDate="vehicle.datumprodaje"
        ></vehicle>
      </ul>
      <div v-else>
        <h1>NA STANJU NEMA VOZILA SA IZABRANIM PARAMETRIMA</h1>
      </div>
    </div>
  </div>
</template>
<script>
//import axios from "axios";

import Vehicle from "./Vehicle";
import FilterVehicles from "./FilterVehicles";
export default {
  components: {
    Vehicle,
    FilterVehicles,
  },

  computed: {
    badFilter() {
      return this.$store.getters.badFilters;
    },
    hasVehicles() {
      return this.$store.getters.showVehicles;
    },
    isLoading() {
      return this.$store.getters.loading;
    },
  },
  data() {
    return {
      vehicles: [],
      selectSorting: 0,
    };
  },

  methods: {
    sortOption() {
      this.$store.dispatch("sortVehicles", { value: this.selectSorting });
    },
    search(
      proizvodjac,
      model,
      cenaod,
      cenado,
      vrstaGoriva,
      kilometrazaod,
      kilometrazado,
      snagaMotoraod,
      snagaMotorado,
      prodato
    ) {
      this.$store.dispatch("filterVehicles", {
        proizvodjacid: proizvodjac,
        modelid: model,
        cenaod: cenaod,
        cenado: cenado,
        vrstaGorivaid: vrstaGoriva,
        kilometrazaod: kilometrazaod,
        kilometrazado: kilometrazado,
        snagaMotoraod: snagaMotoraod,
        snagaMotorado: snagaMotorado,
        prodata: prodato,
      });
    },
  },
  watch: {
    $route() {
      this.search(
        this.$route.query.proizvodjac,
        this.$route.query.model,
        this.$route.query.cenaod,
        this.$route.query.cenado,
        this.$route.query.vrstagoriva,
        this.$route.query.kilometrazaod,
        this.$route.query.kilometrazado,
        this.$route.query.snagamotoraod,
        this.$route.query.snagamotorado,
        this.$route.query.prodata
      );
    },
  },
  mounted() {
    console.log(this.isLoading);
    this.search(
      this.$route.query.proizvodjac,
      this.$route.query.model,
      this.$route.query.cenaod,
      this.$route.query.cenado,
      this.$route.query.vrstagoriva,
      this.$route.query.kilometrazaod,
      this.$route.query.kilometrazado,
      this.$route.query.snagamotoraod,
      this.$route.query.snagamotorado,
      this.$route.query.prodata
    );
  },
};
</script>
<style>
ul {
  display: flex;
  flex-flow: row wrap;
  align-content: space-around;
  width: 90%;
}
.filter {
  width: 60%;
}
.divide {
  width: 90%;
}
.filter,
.divide {
  display: block;
  background-color: rgba(17, 17, 26, 0.05);
  margin: 0 auto;
  margin-top: 30px;
  transition: box-shadow 0.3s ease-in-out;
}
.filter:hover,
.divide:hover {
  box-shadow: rgba(17, 17, 26, 0.05) 0px 1px 0px, orangered 0px 0px 8px;
}
.all {
  display: block;
  margin-top: 10%;
  background-color: white;
}
.sorting {
  display: block;
  top: 20%;
  font-family: "Cabin", sans-serif;
  width: 80%;
  height: 2rem;
}
</style>
