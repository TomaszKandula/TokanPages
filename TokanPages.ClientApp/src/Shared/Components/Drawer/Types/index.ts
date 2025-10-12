export interface DrawerProps {
    isAnonymous: boolean;
    isOpen: boolean;
    onTrigger: () => void;
    children: React.ReactElement;
}
