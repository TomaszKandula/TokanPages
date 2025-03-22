import * as React from "react";
import { CollapsibleView } from "./View/collapsibleView";

interface CollapsibleProps {
    isOpen?: boolean;
    minHeight?: number;
    children: React.ReactNode;
}

export const Collapsible = (props: CollapsibleProps): React.ReactElement => {
    const offset = 30;
    const ref = React.useRef<HTMLDivElement>(null);
    const [isOpen, setIsOpen] = React.useState(props.isOpen);
    const [height, setHeight] = React.useState<number | undefined>(undefined);

    const buttonClickHandler = React.useCallback(() => {
        setIsOpen(!isOpen);
    }, [isOpen]);

    React.useEffect(() => {
        if (isOpen) {
            const currentHeight = ref.current?.getBoundingClientRect().height ?? 0;
            setHeight(currentHeight + offset);
        } else {
            setHeight(props.minHeight ?? 0);
        }
    }, [isOpen]);

    return (
        <CollapsibleView isOpen={isOpen} height={height} clickHandler={buttonClickHandler} reference={ref}>
            {props.children}
        </CollapsibleView>
    );
};
