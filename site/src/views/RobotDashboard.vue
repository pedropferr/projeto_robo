<template>
  <v-container class="d-flex align-center text-center justify-center">
    <v-col cols="10">
      <v-card :loading="loading" :disabled="loading" elevation="0" border>
        <v-row>
          <v-col cols="12">
            <v-card color="blue" class="pa-3">
              <span class="font-weight-bold text-h5"> Painel de Controle </span>
            </v-card>
          </v-col>
          <!-- Controles para cabeça -->
          <v-col cols="12">
            <v-row>
              <v-col cols="12">
                <span class="font-weight-bold text-h6">
                  Controles para cabeça
                </span>
              </v-col>
              <v-col cols="6">
                <v-row class="align-center justify-center">
                  <v-col cols="12" class="pa-0">
                    <span class="font-weight-bold text-h6">
                      <v-icon icon="mdi-rotate-360" />
                      Rotação
                    </span>
                  </v-col>
                  <v-col cols="11" class="mx-5">
                    <v-select
                      v-model="robo.cabeca.rotacao"
                      :items="itensRotacao"
                      item-title="nome"
                      item-value="valor"
                      variant="outlined"
                      flat
                      :error-messages="mensagensErro.rotacao"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-col>
              <v-col cols="6">
                <v-row class="align-center justify-center">
                  <v-col cols="12" class="pa-0">
                    <span class="font-weight-bold text-h6">
                      <v-icon icon="mdi-arrow-up-down" />
                      Inclinação
                    </span>
                  </v-col>
                  <v-col cols="11" class="mx-5">
                    <v-select
                      v-model="robo.cabeca.inclinacao"
                      :items="itensInclinacao"
                      item-title="nome"
                      item-value="valor"
                      variant="outlined"
                      flat
                      :error-messages="mensagensErro.inclinacao"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-col>
            </v-row>
          </v-col>
          <v-divider></v-divider>
          <!-- Controles para os braços -->
          <v-col cols="12" class="mb-2">
            <v-row>
              <v-col cols="12">
                <span class="font-weight-bold text-h6">
                  Controles para os braços
                </span>
              </v-col>
              <v-col cols="6">
                <v-row class="align-center justify-center">
                  <v-col cols="12">
                    <span class="font-weight-bold text-h6">
                      <v-icon icon="mdi-arm-flex" />
                      Braço esquerdo
                    </span>
                  </v-col>
                  <v-col cols="12" class="pa-0">
                    <span class="font-weight-bold"> Cotovelo </span>
                  </v-col>
                  <v-col cols="11" class="mx-5">
                    <v-select
                      v-model="robo.bracoEsquerdo.cotovelo"
                      :items="itensCotovelo"
                      item-title="nome"
                      item-value="valor"
                      variant="outlined"
                      flat
                      :error-messages="mensagensErro.cotoveloEsquerdo"
                      @input="mensagensErro.cotoveloEsquerdo = ''"
                    ></v-select>
                  </v-col>
                  <v-col cols="12" class="pa-0">
                    <span class="font-weight-bold"> Pulso </span>
                  </v-col>
                  <v-col cols="11" class="mx-5">
                    <v-select
                      v-model="robo.bracoEsquerdo.pulso"
                      :items="itensPulso"
                      item-title="nome"
                      item-value="valor"
                      variant="outlined"
                      flat
                      :error-messages="mensagensErro.pulsoEsquerdo"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-col>
              <v-col cols="6">
                <v-row class="align-center justify-center">
                  <v-col cols="12">
                    <span class="font-weight-bold text-h6">
                      Braço direito
                      <v-icon
                        icon="mdi-arm-flex"
                        style="transform: scaleX(-1)"
                      />
                    </span>
                  </v-col>
                  <v-col cols="12" class="pa-0">
                    <span class="font-weight-bold"> Cotovelo </span>
                  </v-col>
                  <v-col cols="11" class="mx-5">
                    <v-select
                      v-model="robo.bracoDireito.cotovelo"
                      :items="itensCotovelo"
                      item-title="nome"
                      item-value="valor"
                      variant="outlined"
                      flat
                      :error-messages="mensagensErro.cotoveloDireito"
                    ></v-select>
                  </v-col>
                  <v-col cols="12" class="pa-0">
                    <span class="font-weight-bold"> Pulso </span>
                  </v-col>
                  <v-col cols="11" class="mx-5">
                    <v-select
                      v-model="robo.bracoDireito.pulso"
                      :items="itensPulso"
                      item-title="nome"
                      item-value="valor"
                      variant="outlined"
                      flat
                      :error-messages="mensagensErro.pulsoDireito"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-col>
            </v-row>
          </v-col>
        </v-row>
        <v-col cols="12">
          <v-btn
            color="blue"
            prepend-icon="mdi-robot-happy"
            variant="tonal"
            size="large"
            @click="autalizaRobo()"
          >
            Atualizar robo
          </v-btn>
        </v-col>
      </v-card>
    </v-col>
  </v-container>
</template>

<script>
import valoresRotacao from "../enums/valoresRotacao";
import valoresInclinacao from "../enums/valoresInclinacao";
import valoresCotovelo from "../enums/valoresCotovelo";
import valoresPulso from "../enums/valoresPulso";

export default {
  data: () => ({
    loading: false,
    valoresRotacao,
    valoresInclinacao,
    valoresCotovelo,
    valoresPulso,
    robo: {
      cabeca: {
        rotacao: null,
        inclinacao: null,
      },
      bracoDireito: {
        cotovelo: null,
        pulso: null,
      },
      bracoEsquerdo: {
        cotovelo: null,
        pulso: null,
      },
    },
    mensagensErro: {
      cotoveloDireito: "",
      cotoveloEsquerdo: "",
      inclinacao: "",
      pulsoDireito: "",
      pulsoEsquerdo: "",
      rotacao: "",
      possuiErros: false,
    },
    itensRotacao: [
      {
        nome: "Rotação -90º",
        valor: valoresRotacao.RotacaoNegativa90,
      },
      {
        nome: "Rotação -45º",
        valor: valoresRotacao.RotacaoNegativa45,
      },
      {
        nome: "Em Repouso",
        valor: valoresRotacao.EmRepouso,
      },
      {
        nome: "Rotação 45º",
        valor: valoresRotacao.Rotacao45,
      },
      {
        nome: "Rotação 90º",
        valor: valoresRotacao.Rotacao90,
      },
    ],
    itensInclinacao: [
      {
        nome: "Para Cima",
        valor: valoresInclinacao.ParaCima,
      },
      {
        nome: "Em Repouso",
        valor: valoresInclinacao.EmRepouso,
      },
      {
        nome: "Para Baixo",
        valor: valoresInclinacao.ParaBaixo,
      },
    ],
    itensCotovelo: [
      {
        nome: "Em Repouso",
        valor: valoresCotovelo.EmRepouso,
      },
      {
        nome: "Levemente Contraído",
        valor: valoresCotovelo.LevementeContraido,
      },
      {
        nome: "Contraído",
        valor: valoresCotovelo.Contraido,
      },
      {
        nome: "Fortemente Contraído",
        valor: valoresCotovelo.FortementeContraido,
      },
    ],
    itensPulso: [
      {
        nome: "Rotação para -90º",
        valor: valoresPulso.RotacaoNegativa90,
      },
      {
        nome: "Rotação para -45º",
        valor: valoresPulso.RotacaoNegativa45,
      },
      {
        nome: "Em Repouso",
        valor: valoresPulso.EmRepouso,
      },
      {
        nome: "Rotação para 45º",
        valor: valoresPulso.Rotacao45,
      },
      {
        nome: "Rotação para 90º",
        valor: valoresPulso.Rotacao90,
      },
      {
        nome: "Rotação para 135º",
        valor: valoresPulso.Rotacao135,
      },
      {
        nome: "Rotação para 180º",
        valor: valoresPulso.Rotacao180,
      },
    ],
  }),
  mounted() {
    this.pegaRobo();
  },
  methods: {
    pegaRobo() {
      this.loading = true;
      fetch("https://localhost:7126/Robo/PegaRobo")
        .then((response) => response.json())
        .then((data) => {
          this.robo = data;
          this.loading = false;
        })
        .catch((error) => {
          this.loading = false;
          console.error("Erro ao consultar a API:", error);
        });
    },
    autalizaRobo() {
      this.loading = true;
      fetch("https://localhost:7126/Robo/AtualizaRobo", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(this.robo),
      })
        .then((response) => response.json())
        .then((data) => {
          this.mensagensErro = data;
          this.loading = false;
          if(!this.mensagensErro.possuiErros)
             this.pegaRobo();
        })
        .catch((error) => {
          this.loading = false;
          console.error("Erro ao consultar a API:", error);
        });
    },
  },
};
</script>
