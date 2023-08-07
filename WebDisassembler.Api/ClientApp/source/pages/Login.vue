<script setup lang="ts">

import {ref} from "vue";
import { useAuthenticationClient } from "@/api/http-clients.ts";
import {useProfileStore} from "@/stores/profile.ts";
import {useRouter} from "vue-router";

const usernameOrEmail = ref('');
const password = ref('');

const profileStore = useProfileStore();
const authClient = useAuthenticationClient();
const router = useRouter();

const onSubmit = async e =>
{
  e.preventDefault();
  const result = await authClient.login({
    usernameOrEmail: usernameOrEmail.value,
    password: password.value
  });
  
  if (result.ok)
  {
    await profileStore.update();
    await router.push("/profile")
  }
}
</script>

<template>
  <div class="page">
    <form @submit="onSubmit">
      Login
      <span>
        <label>Email / Username:</label>
        <input v-model="usernameOrEmail" type="text" />
      </span>
      <span>
        <label>Password:</label>
        <input v-model="password" type="password" />
      </span>
      <input value="Login" type="submit" />
    </form>
  </div>
</template>

<style scoped>
  .page { 
    @apply flex justify-center items-center h-full; 
    form {
      @apply p-4 flex flex-col;
      background: var(--bg-normal);
      
      span {
        @apply my-2 flex justify-between;
        input { @apply ml-2; }
      }
    }
  }
</style>