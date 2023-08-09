<template>
    <div class="button">
        <slot />
    </div>
</template>

<style>
.button {
    @apply px-6 py-2 m-1 text-center bg-red-500 border-3 rounded-xl;
    background: var(--button-secondary);
    border-color: var(--button-primary);
    color: var(--button-primary);

    transition-properties: color, background-color, border-color;
    transition-duration: .25s;
    transition-timing-function: ease-out;

    --button-primary: var(--text-normal);
    --button-secondary: var(--bg-base);
}

.button:hover {
    background: var(--button-primary);
    border-color: var(--button-primary);
    color: var(--button-secondary);

    cursor: pointer;
}

.button.button-success {
    --button-primary: var(--success);
    --button-secondary: var(--bg-base);
}

.button.button-error {
    --button-primary: var(--error);
    --button-secondary: var(--bg-base);
}

.button-group {
    @apply flex flex-row;
}
</style>