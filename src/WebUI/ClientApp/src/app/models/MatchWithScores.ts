import { ScoreDto } from '../web-api-client';

export interface MatchWithScores {
  matchIdEncoded: string;
  scores: ScoreDto[];
}

