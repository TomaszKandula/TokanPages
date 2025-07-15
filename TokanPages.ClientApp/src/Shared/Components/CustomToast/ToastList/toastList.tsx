import React from "react";
import { ToastData, ToastListProps } from "../Types";
import { Toast } from "../Toast";
import "./toastList.css";

export const ToastList = (props: ToastListProps): React.ReactElement => {
    const listRef = React.useRef<HTMLDivElement | null>(null);

    const sortedData = props.position.includes("bottom") ? [...props.data].reverse() : [...props.data];

    const handleScrolling = React.useCallback(
        (element: HTMLDivElement | null) => {
            const isTopPosition = ["top-left", "top-right"].includes(props.position);
            if (isTopPosition) {
                element?.scrollTo(0, element.scrollHeight);
            } else {
                element?.scrollTo(0, 0);
            }
        },
        [props.position]
    );

    const handleToastRemoval = React.useCallback((id: number) => {
        props.removeToast(id);
    }, []);

    React.useEffect(() => {
        handleScrolling(listRef.current);
    }, [props.position, props.data]);

    if (sortedData.length < 1) {
        return <></>;
    }

    return (
        <div
            data-testid="toaster-list-view"
            className={`toast-list toast-list--${props.position}`}
            aria-live="assertive"
            ref={listRef}
        >
            {sortedData.map((toast: ToastData) => (
                <Toast
                    id={toast.id}
                    key={toast.id}
                    message={toast.message}
                    type={toast.type}
                    onClose={handleToastRemoval}
                />
            ))}
        </div>
    );
};
