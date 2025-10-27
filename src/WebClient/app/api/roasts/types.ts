export interface Roast {
  text: string;
  speech?: string;
}

export interface RoastRequest {
  imageBytes: string;
}

export interface RoastResponse {
  completionText: string;
  speechBytes?: string;
}
