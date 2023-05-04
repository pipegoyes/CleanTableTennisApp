import { TeamInformation } from '../team-information/team-information.component';
import { Players } from "./Players";


export interface WizardInformation {
  teamInformation: TeamInformation;
  hostPlayers: Players;
  guestPlayers: Players;
  hostFirstDouble: string[];
  hostSecondDouble: string[];
  guestFirstDouble: string[];
  guestSecondDouble: string[];
}
