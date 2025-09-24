# Estudo sobre Máquina de Estados
Eu fiz este projeto para estudar a implentação e comportamento do padrão de design (design pattern) máquina de estados (state machine).

Minhas princiapais referências foram os vídeos do canal [IHeartGameDev](https://www.youtube.com/c/iHeartGameDev/)
- [Conceito Teórico sobre Máquina de Estados](https://www.youtube.com/watch?v=Vt8aZDPzRjI)
- [Máquina de Estados na prática](https://www.youtube.com/watch?v=kV06GiJgFhc)

## Tecnologias
- Unity 2021.3.18f
  - Cinemachine
  - Timeline
- Visual Studio 2022

## Como fazer o setup
1. Abra o projeto na unity 2021.3.18f
2. Instale com packpage manager da Unity do Cinemachine e a Timeline
3. Coloque a visualização do game em resolução 16:9
4. Na cena "ComStateMachine" verifique se o o gameobject "CameraFollow" tem o script "CameraSegueIsto"
5. Verique se o script "CameraSegueIsto" tem a referência do Player no campo PlayerTransform
6. O player deve estar com o seguinte script com essas seguintes configurações 
<img width="445" height="343" alt="image" src="https://github.com/user-attachments/assets/5a3df0ff-bb38-46b7-99bf-357744ca743b" />

7. Já na cena SemStateMachine o player deve ter os seguintes scripts configurados
<img width="439" height="456" alt="image" src="https://github.com/user-attachments/assets/1974d2ee-736a-459e-a3d5-ffa5bd4b82c5" />

## Como funciona
O projeto tem 2 cenas: a "ComStateMachine" que contém o Player se movendo com máquina de estados. Já a "SemStateMachine" tem um script mais tradicional do movimento do Player.
A cena "ComStateMachine" também possui uma configuração do cinemachine para seguir o jogador e é possível ver no canto esquerdo .
Também há 2 prefabs para o player: "PlayerComStateMachine" e "PlayerSemmStateMachine", cada um configurado 
Os controles são as SETAS/WASD para movimento, ESPAÇO para fulgar é letra Q para dar Dash
Cada um dos scripts possui comentários e mostra a diferença entre usar state machine ou não

## Possíveis Problemas
O player sem máquina de estados pode entrar dentro da parede se você ficar tentando usar o Dash em direção a parede várias vezes seguidas.
Se você pular próximo a uma parede e no ar se mover em direção a ela, o jogador vai 'grudar' na parede e  não vai cair devido a força de atrito até que o movimento em direção da parede pare.

## Próximos Passos
Adicionar a verificação para o jogador não grudar na parede durante pulos.
Adicionar cinemachine no player sem máquina de estados.
Adicionar trail render no player sem máquina de estados.
Quem sabe criar um jogo sobre state machine 😄 
