import { TSeverity } from "Shared/types";

export interface LogMessageDto {
    eventDateTime: string;
    eventType: string;
    severity: TSeverity;
    message: string;
    stackTrace: string;
    pageUrl: string;
    userAgent: string;
    clientData: {
        browser: {
            major?: string;
            name?: string;
            type?: string;
            version?: string;
        };
        cpu: {
            architecture?: string;
        };
        device: {
            model?: string;
            type?: string;
            vendor?: string;
        };
        engine: {
            name?: string;
            version?: string;
        };
        os: {
            name?: string;
            version?: string;
        };
    };
}
