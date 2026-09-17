# ⚔️ Platformer 2D - Action & Exploration

![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

> **Status do Projeto:** Em desenvolvimento ativo 🚀
>
![Meu GIF animado](Assets\Animação.gif)

## 📖 Sobre o Projeto
Um protótipo de jogo de plataforma 2D focado em movimentação fluida e combate melee preciso, desenvolvido na **Unity (C#)**. Este projeto serve como um laboratório prático para aprofundar conhecimentos em arquitetura de código, física de jogos e engenharia de software aplicada ao Game Design.

O objetivo principal deste repositório é demonstrar a criação de sistemas modulares e escaláveis, evitando o acoplamento de código e priorizando o uso inteligente das ferramentas nativas da engine.

## 🛠️ Destaques Técnicos & Sistemas Implementados

### 🏃 Movimentação e Física (Rigidbody2D)
* **Controle Físico Preciso:** A movimentação foi construída utilizando manipulação direta de velocidade no `FixedUpdate`, garantindo consistência na taxa de quadros e evitando "stutters".
* **Dash com Desaceleração Matemática:** Implementação de mecânica de evasão (Dash) que utiliza `Mathf.Lerp` dentro de uma *Coroutine* para gerar uma transição suave de velocidade, anulando a gravidade temporariamente.
* **Sistema de Pulo Duplo:** Controle dinâmico de inércia vertical e uso de sobreposição de colisão (OverlapCircle) para verificação de chão.

### ⚔️ Sistema de Combate Melee
* **Hitboxes Dinâmicas:** Uso de `Physics2D.OverlapCircleAll` e filtragem por *LayerMasks* para garantir que os cálculos de dano ocorram apenas nos objetos desejados (otimização de processamento).
* **Sincronia Arte-Código:** Integração profunda entre os scripts e o *Animator* utilizando **Animation Events**. O dano e as transições de estado (como a destruição de inimigos) ocorrem em frames específicos da animação, gerando um excelente *Game Feel*.
* **Coletáveis e Gatilhos lógicos:** Sistema de *Triggers* (OnTriggerEnter2D) acoplado a variáveis de estado (`bool`) para desbloqueio de novas mecânicas (como a obtenção da espada).

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
