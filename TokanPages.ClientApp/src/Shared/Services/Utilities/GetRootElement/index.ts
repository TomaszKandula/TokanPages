export const GetRootElement = (): HTMLElement => {
    const root = document.getElementById("root");
    if (root === null) {
        throw new Error("Unexpected internal error!");
    }

    return root;
};
