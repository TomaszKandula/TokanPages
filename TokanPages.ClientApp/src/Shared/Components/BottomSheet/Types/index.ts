export interface BottomSheetProps {
    isOpen: boolean;
    caption: string;
    onTrigger: () => void;
    children: React.ReactElement | React.ReactElement[];
}
