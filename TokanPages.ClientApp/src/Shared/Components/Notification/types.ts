export type Mode = "link" | "primary" | "info" | "success" | "warning" | "danger";

export interface NotificationProps {
    text: string;
    className?: string;
    mode?: Mode;
    isLight?: boolean;
    hasIcon?: boolean;
    onClose?: () => void;
}
