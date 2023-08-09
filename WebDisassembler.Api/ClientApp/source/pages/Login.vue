<script setup lang="ts">

import {ref} from "vue";
import { useAuthenticationClient } from "@/api/http-clients.ts";
import {useProfileStore} from "@/stores/profile.ts";
import {useRouter} from "vue-router";
import Form from "@/ui/form/Form.vue";
import Button from "@/ui/misc/Button.vue";
import TextField from "@/ui/form/TextField.vue";

const profileStore = useProfileStore();
const authClient = useAuthenticationClient();
const router = useRouter();

const formElement = ref<Form>();

const onSubmit = async props =>
{
  console.log(props);
  alert('ey');
  const result = await authClient.login({
    usernameOrEmail: props.usernameOrEmail,
    password: props.password
  });
  
  if (result.ok)
  {
    alert('Login success');
    await profileStore.update();
    await router.push("/profile")
  }
}
</script>

<template>
  <div class="page">
    <Form ref="formElement" @submit="onSubmit" >
      <div>
        <label>Email / Username:</label>
        <TextField name="usernameOrEmail" />
      </div>
      <div>
        <label>Password:</label>
        <TextField name="password" type="password" />
      </div>
      <Button @click="formElement.submit();">Login</Button>
    </Form>
  </div>
</template>

<style scoped>
  .page { 
    @apply flex justify-center items-center h-full; 
    form {
      @apply p-4 flex flex-col;
      background: var(--bg-base);
      
      span {
        @apply my-2 flex justify-between;
        input { @apply ml-2; }
      }
    }
  }
</style>