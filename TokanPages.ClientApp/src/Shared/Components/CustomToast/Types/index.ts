interface ToastBaseProps {
    message: string;
    type: MessageType;
}

export interface ToastData extends ToastBaseProps {
    id: number;
}

export interface ToastProps extends ToastData {
    onClose: (id: number) => void;
}

export interface ToastListProps {
    data: ToastData[];
    position: Position;
    removeToast: (id: number) => void;
}

export interface IconMapProps {
    success: React.ReactElement;
    failure: React.ReactElement;
    warning: React.ReactElement;
}

export type Position = "top-right" | "top-left" | "bottom-right" | "bottom-left";
export type MessageType = "success" | "failure" | "warning";

export interface NotificationToasterProps {
    hasAutoClose: boolean;
    AutoCloseDurationSec: number;
    position?: Position;
}
