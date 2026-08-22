# 🐾 Darwin - The Shape-Shifting Adventure

Um jogo de plataforma e puzzle 2D desenvolvido na **Unity Engine** com **C#**. Em **Darwin**, o jogador controla um protagonista com a habilidade de se transformar em diferentes criaturas e objetos, adaptando suas mecânicas para superar desafios de cenário, perseguições em alta velocidade, combates contra chefes e uma mecânica única de ressurreição no Limbo.

---

## 📌 Visão Geral e Mecânica de Segunda Chance

Ao longo de 5 fases interligadas, Darwin precisa utilizar a adaptação ao ambiente para sobreviver aos perigos do cenário. O jogo conta ainda com o **Sistema de Limbo (Segunda Chance)**:
* Ao sofrer a primeira derrota na fase, Darwin não é eliminado imediatamente: ele é enviado para o **Limbo**, onde precisa coletar as **Estrelas da Vida** para reviver e retornar ao início da fase em que estava.
* Essa mecânica funciona apenas uma vez: se morrer novamente após o retorno, todo o progresso é reiniciado (*Permadeath / Reset Global*).

---

## 🗺️ Fases e Habilidades de Transformação

### 🌲 Fase 1: A Floresta e a Adaptação Natural
Darwin inicia sua jornada precisando desbloquear e alternar entre diferentes formas biológicas para avançar:
* **Gato Guerreiro:** Combate corpo a corpo utilizando espada contra criaturas hostis.
* **Sapo:** Salto de longa distância para transpor abismos e plataformas elevadas.
* **Javali:** Investida pesada capaz de quebrar obstáculos sólidos no caminho.
* **Lesma / Caracol:** Redução de tamanho corporal para atravessar frestas estreitas e passagens secretas.
* *Conclusão:* Acesso e queda em uma caverna misteriosa.

---

### 🪨 Fase 2: O Colapso da Caverna
Um estágio focado em *precision platforming* e tempo de reação:
* Fuga em velocidade constante de uma parede de espinhos em perseguição.
* Desvios de estalagmites, espinhos no solo e saltos de alta precisão até alcançar as portas da Masmorra.

---

### 🏰 Fase 3: A Masmorra Mágica & Batalha Contra o Dragão
Dentro da fortaleza ancestral, as transformações assumem formas místicas e mecânicas:
* **Livro Mágico:** Capacidade de voo contínuo para alcançar plataformas verticais distantes.
* **Armadura Encantada:** Alta resistência e ataques corpo a corpo para enfrentar guardas da masmorra.
* **Esfregão Mágico:** Limpeza de poças e gosmas tóxicas que bloqueiam a passagem do jogador.
* *Boss Fight:* Confronto contra o Dragão ancestral ao final da masmorra.

---

### 🔥 Fase 4: Fuga da Lava (Escape Sequence)
Com a queda do Dragão, a masmorra entra em colapso e começa a se encher rapidamente de lava:
* Desafio vertical de pulos cronometrados sob pressão de tempo até alcançar a superfície.

---

### ☀️ Fase 5: O Retorno à Floresta
Darwin consegue escapar do complexo subterrâneo, retornando à superfície para reencontrar seus amigos animais e restaurar a paz na floresta.

---

## 🛠️ Tecnologias e Conceitos Aplicados

* **Engine:** Unity 2D
* **Linguagem:** C# (.NET)
* **Arquitetura de Software:** Padrão *State Machine* (Máquina de Estados) para gerenciamento das transições entre as transformações e seus respectivos conjuntos de animação.
* **Física & Colisão:** Uso de `Rigidbody2D`, `BoxCollider2D`, `CircleCollider2D` e *Raycasting* para detecção de chão, colisores dinâmicos e mecânicas de destruição de obstáculos.
* **Sistemas de Jogo:** Gerenciamento do ciclo de vida da sessão, sistema de pontuação/coleta no Limbo e controle de checkpoint condicional (1 chance de renascimento por run).

---

## 🎮 Controles Padrão (Teclado)

* **A / D:** Movimentação lateral
* **A / D / S / W:** Movimentação do voo (na forma livro)
* **Espaço:** Salto / Ataque (na forma gato)
* **E:** Alternar entre as transformações disponíveis (após encostar nos personagens)

---

## 🚀 Como Jogar

1. Clone o repositório ou faça o download como ZIP:
```bash
git clone [https://github.com/1gabrielv/Projeto-Jogo-2D-Unity---Darwin.git](https://github.com/1gabrielv/Projeto-Jogo-2D-Unity---Darwin.git)](https://github.com/1gabrielv/Projeto-Jogo-2D-Unity---Darwin.git)
```

2. Acesse a pasta do projeto baixado.
3. Execute o arquivo executável (`.exe`) do jogo para iniciar a partida imediatamente.

---
