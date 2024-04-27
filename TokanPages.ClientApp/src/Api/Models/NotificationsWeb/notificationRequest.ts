export interface NotificationRequest {
    canSkipPreservation?: boolean;
    userId: string;
    handler: string;
    externalPayload?: string;
}
