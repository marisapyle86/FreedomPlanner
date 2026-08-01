import type { ReactNode } from 'react'

type DashboardPageProps = {
  children: ReactNode
}

function DashboardPage({ children }: DashboardPageProps) {
  return <main className="dashboard-shell">{children}</main>
}

export default DashboardPage
