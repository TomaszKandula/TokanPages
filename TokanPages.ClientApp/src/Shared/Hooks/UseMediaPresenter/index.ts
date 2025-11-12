import React from "react";

interface UseMediaPresenterReturnProps {
    isPresenterOpen: boolean;
    selection: number;
    onSelectionClick: (selection: number) => void;
    onPresenterClick: () => void;
}

export const useMediaPresenter = (): UseMediaPresenterReturnProps => {
    const [isPresenterOpen, setIsImageOpen] = React.useState(false);
    const [selection, setSelection] = React.useState(0);

    const onSelectionClick = React.useCallback((selection: number) => {
        setSelection(selection);
        onPresenterClick();
    }, []);

    const onPresenterClick = React.useCallback(() => {
        setIsImageOpen(!isPresenterOpen);
    }, [isPresenterOpen]);

    return {
        isPresenterOpen: isPresenterOpen,
        selection: selection,
        onSelectionClick: onSelectionClick,
        onPresenterClick: onPresenterClick,
    };
};
