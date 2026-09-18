
# ⚔️ Platformer 2D - Action & Exploration

![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

> **Status do Projeto:** Em desenvolvimento ativo 🚀
><img width="721" height="377" alt="Gif" src="https://github.com/user-attachments/assets/c6d4b634-453f-4d6d-8419-47ca91d6ed2c" />


## 📖 Sobre o Projeto
Um protótipo de jogo de plataforma 2D focado em movimentação fluida e combate melee preciso, desenvolvido na **Unity (C#)**. Este projeto serve como um laboratório prático para aprofundar conhecimentos em arquitetura de código, física de jogos e engenharia de software aplicada ao Game Design.

O objetivo principal deste repositório é demonstrar a criação de sistemas modulares e escaláveis, evitando o acoplamento de código e priorizando o uso inteligente das ferramentas nativas da engine.

## 🛠️ Destaques Técnicos & Sistemas Implementados

### 🧠 Inteligência Artificial & Inimigos (Enemy AI)
* **Máquina de Estados Simples:** Inimigos tomam decisões baseadas em distância (visão vs. alcance de ataque), alternando fluidamente entre perseguição (com saltos físicos sobre obstáculos) e ataques baseados em cooldowns.
* **UI Espacial (World Space Canvas):** Implementação de barras de vida dinâmicas acopladas aos inimigos, com preenchimento calculado matematicamente `(float)ActualLife / MaxLife` e atualizado em tempo real.

### ⚔️ Sistema de Combate Avançado
* **Hitboxes Matemáticas Bidirecionais:** As áreas de colisão de dano (`Physics2D.OverlapCircleAll`) são projetadas matematicamente via código através de multiplicadores de direção. Isso isola a física da interface visual (`Animator`), prevenindo bugs de travamento de *Transform* causados por animações.
* **Sistema de I-Frames (Frames de Invencibilidade):** Uso nativo de `IEnumerator` (Coroutines) acoplado a colisões contínuas (`OnCollisionStay2D`) para criar janelas de invulnerabilidade no jogador, garantindo um balanceamento justo e evitando múltiplos hits de contato em um único frame.
* **Sincronia Arte-Código:** Integração profunda utilizando **Animation Events**. A aplicação de dano e a destruição de *GameObjects* (morte) ocorrem de forma assíncrona, aguardando frames específicos das animações para maximizar o *Game Feel*.

### 🏃 Movimentação e Física (Rigidbody2D)
* **Controle Físico Preciso:** A movimentação foi construída utilizando manipulação direta de velocidade no `FixedUpdate`.
* **Mecânica de Evasão (Dash):** Implementação de *Dash* que utiliza `Mathf.Lerp` dentro de uma *Coroutine* para gerar uma desaceleração matemática suave, alterando a escala de gravidade dinamicamente.
## 🎮 Como Testar o Projeto

Para abrir e testar este projeto na sua máquina:

1. Certifique-se de ter a **Unity Editor** instalada (Projeto desenvolvido na versão 6000.6.0f1).
2. Clone este repositório no seu terminal:
   ```bash
   git clone [https://github.com/josevitoraoliveira/Platformer2D.git](https://github.com/josevitoraoliveira/Platformer2D.git)
   Abra o Unity Hub, clique em Add e selecione a pasta clonada.

3. Na Unity, abra a cena principal navegando até Assets/Scenes/SampleScene.unity.

4. Dê Play!

Desenvolvido por José Vitor de Araújo Oliveira 

Apaixonado por lógica, resolução de problemas e Game Development.
