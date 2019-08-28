<template>
  <el-container>
    <el-aside width="200px">
      <navegationmenu></navegationmenu>
    </el-aside>
    <el-container>
      <el-header class="bg-info"><h1>Cabecera</h1></el-header>
      <el-main>
        <el-row>
          <el-col :span="24">
            <router-view></router-view>
          </el-col>
        </el-row>
      </el-main>
      <!-- <el-footer>Pie de página</el-footer> -->
    </el-container>
  </el-container>
</template>

<script>
import navegationmenu from "@/components/shared/NavegationMenu";

export default {
  components: {
    navegationmenu
  },
  props: {
    source: String
  }
};
</script>