using RogueCS.Engine.Interfaces;

namespace RogueCS.Engine.Implementations.StateHandlers {
    public class DungeonStateHandler : IStateHandler {

        GameState HandledState => GameState.Dungeon;

      #region ctor

        DungeonStateHandler() {}

      #endregion ctor

      #region private

        // Movement
        void CommandMoveUp(GameData data) {
            var newLocation = data.Player.Location;
            newLocation.y--;

            data.Player.SetLocation(in newLocation);
        }
        
        void CommandMoveDown(GameData data) {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x,
                currentLocation.y + 1
            });
        }
        
        void DungeonStateHandler::CommandMoveLeft(GameData* pData) const {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x - 1,
                currentLocation.y
            });
        }
        
        void DungeonStateHandler::CommandMoveRight(GameData* pData) const {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x + 1,
                currentLocation.y 
            });
        }
        
        void DungeonStateHandler::CommandMoveUL(GameData* pData) const {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x - 1,
                currentLocation.y + 1
            });
        }
        
        void DungeonStateHandler::CommandMoveUR(GameData* pData) const {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x + 1,
                currentLocation.y - 1
            });
        }
        void DungeonStateHandler::CommandMoveLR(GameData* pData) const {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x + 1,
                currentLocation.y + 1
            });
        }
        
        void DungeonStateHandler::CommandMoveLL(GameData* pData) const {
        
            auto currentLocation = pData->GetPlayer().GetLocation();
        
            pData->GetPlayer().SetLocation({
                currentLocation.x - 1,
                currentLocation.y + 1
            });
        }
      #endregion private


		// Inherited via IStateHandler
		virtual void EnteringState() override;
		virtual override GameState HandleGameLoop(SDL_Event e, GameData data) {

		}

		virtual override GameState GetHandledState() const noexcept override { return HANDLED_STATE; }
    }
}

